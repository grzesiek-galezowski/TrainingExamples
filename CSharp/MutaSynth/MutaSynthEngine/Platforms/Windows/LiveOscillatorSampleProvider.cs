using NAudio.Wave;
using System.Diagnostics;

namespace MutaSynthEngine.Platforms.Windows;

public sealed class LiveOscillatorSampleProvider : ISampleProvider
{
    private const float BaseKeytrackFrequency = 440.0f;
    private const float MinimumFilterCutoffFrequency = 20.0f;
    private const float MinimumFilterCutoffNormalized = 0.0001f;
    private const double TwoPi = Math.PI * 2.0;
    private const int AttackDurationMilliseconds = 2;
    private const int ReleaseDurationMilliseconds = 12;
    private const float FixedMixHeadroomGain = 0.5f;
    private const float LargeDeltaThreshold = 0.02f;
    private const int DiagnosticWindowSampleCount = 8;
    private readonly WaveFormat _waveFormat;
    private readonly object _sync = new();
    private readonly Dictionary<float, VoiceState> _voices = [];
    private readonly List<float> _completedVoices = [];
    private readonly Queue<SampleSnapshot> _recentSamples = [];
    private readonly List<PendingVoiceCompletionLog> _pendingVoiceCompletionLogs = [];
    private float _volume;
    private OscillatorWaveform _waveform = OscillatorWaveform.Saw;
    private int _semiOffset;
    private int _centsOffset;
    private int _bitRedux;
    private int _keytrackPercent = 100;
    private FilterType _filterType = FilterType.LpLdr12;
    private float _filterCutoff = 128.0f;
    private float _filterResonance;
    private int _filterKeytrackPercent = 100;
    private float _filterDrive;
    private FilterDriveRoute _filterDriveRoute = FilterDriveRoute.Pre;
    private bool _isOscillatorLoggingEnabled;
    private MoogLadderFilter? _ladderFilter;
    private float _lastTrackedFilterFrequency = BaseKeytrackFrequency;
    private float _previousSample;
    private long _sampleFrameIndex;

    public LiveOscillatorSampleProvider(int sampleRate, int channelCount, float volume)
    {
        _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount);
        _volume = volume;
        _ladderFilter = new MoogLadderFilter(sampleRate);
    }

    public int FormantVowOrder { get; set; } = 0;
    public float FormantControl { get; set; } = 64.0f;

    public WaveFormat WaveFormat => _waveFormat;

    public float Volume
    {
        get
        {
            lock (_sync)
            {
                return _volume;
            }
        }
        set
        {
            lock (_sync)
            {
                _volume = value;
            }
        }
    }

    public void SetOscillatorParameters(OscillatorWaveform waveform, int semiOffset, int centsOffset, int bitRedux, int keytrackPercent, bool isOscillatorLoggingEnabled)
    {
        lock (_sync)
        {
            _waveform = waveform;
            _semiOffset = semiOffset;
            _centsOffset = centsOffset;
            _bitRedux = bitRedux;
            _keytrackPercent = keytrackPercent;
            _isOscillatorLoggingEnabled = isOscillatorLoggingEnabled;
        }
    }

    public void SetFilterParameters(FilterType filterType, float cutoff, float resonance, int keytrackPercent, float drive, FilterDriveRoute driveRoute)
    {
        lock (_sync)
        {
            _filterType = filterType;
            _filterCutoff = cutoff;
            _filterResonance = resonance;
            _filterKeytrackPercent = keytrackPercent;
            _filterDrive = drive;
            _filterDriveRoute = driveRoute;
        }
    }

    public void NoteOn(float frequency)
    {
        lock (_sync)
        {
            _voices[frequency] = new VoiceState(frequency, GetAttackSampleCount());
            _lastTrackedFilterFrequency = frequency;
            Log($"NoteOn freq={frequency:F2} voices={_voices.Count}");
        }
    }

    public void NoteOff()
    {
        lock (_sync)
        {
            foreach (var voice in _voices.Values)
            {
                voice.BeginRelease(GetReleaseSampleCount());
            }

            Log($"NoteOff all voices={_voices.Count}");
        }
    }

    public void NoteOff(float frequency)
    {
        lock (_sync)
        {
            if (_voices.TryGetValue(frequency, out var voice))
            {
                voice.BeginRelease(GetReleaseSampleCount());
                Log($"NoteOff freq={frequency:F2} voices={_voices.Count}");
            }
        }
    }

    public int Read(float[] buffer, int offset, int count)
    {
        lock (_sync)
        {
            var channels = _waveFormat.Channels;
            var frameCount = count / channels;

            for (var frame = 0; frame < frameCount; frame++)
            {
                var sample = MixVoices();

                for (var channel = 0; channel < channels; channel++)
                {
                    buffer[offset + (frame * channels) + channel] = sample;
                }
            }
        }

        return count;
    }

    private float MixVoices()
    {
        var mixedSample = 0.0f;
        var completedVoiceDiagnostics = _isOscillatorLoggingEnabled ? new List<CompletedVoiceDiagnostic>() : null;
        _completedVoices.Clear();

        foreach (var pair in _voices)
        {
            var voice = pair.Value;
            var sample = CreateWaveSample(voice.Phase) * voice.Level;
            var levelBeforeReleaseAdvance = voice.Level;
            mixedSample += ApplyBitReduction(sample);

            voice.Phase += CalculateEffectiveFrequency(voice.BaseFrequency) / _waveFormat.SampleRate;
            if (voice.Phase >= 1.0)
            {
                voice.Phase -= Math.Floor(voice.Phase);
            }

            if (voice.IsReleasing)
            {
                voice.AdvanceRelease();
                if (voice.Level <= 0.0f)
                {
                    _completedVoices.Add(pair.Key);
                    completedVoiceDiagnostics?.Add(new CompletedVoiceDiagnostic(pair.Key, levelBeforeReleaseAdvance));
                }
            }
            else
            {
                voice.AdvanceAttack();
            }
        }

        var mixGainBeforeRemoval = FixedMixHeadroomGain;

        foreach (var completedVoice in _completedVoices)
        {
            _voices.Remove(completedVoice);
        }

        var remainingVoiceCount = _voices.Count;
        var mixGainAfterRemoval = FixedMixHeadroomGain;
        if (remainingVoiceCount > 0)
        {
            _lastTrackedFilterFrequency = GetTrackedFilterFrequency();
        }

        var preAmplifierSample = ApplyFilter(mixedSample * FixedMixHeadroomGain, remainingVoiceCount == 0);
        var outputSample = preAmplifierSample * _volume;
        var sampleDelta = MathF.Abs(outputSample - _previousSample);
        var activeVoiceFrequencies = _isOscillatorLoggingEnabled ? FormatActiveVoiceFrequencies() : string.Empty;
        var currentSampleSnapshot = new SampleSnapshot(_sampleFrameIndex, outputSample, sampleDelta, remainingVoiceCount, FixedMixHeadroomGain);

        CaptureRecentSample(currentSampleSnapshot);
        FlushPendingVoiceCompletionLogs(currentSampleSnapshot);

        if (_isOscillatorLoggingEnabled)
        {
            if (sampleDelta > LargeDeltaThreshold)
            {
                Log($"LargeDelta frame={_sampleFrameIndex} delta={sampleDelta:F4} sample={outputSample:F4} previous={_previousSample:F4} voices={remainingVoiceCount} gain={FixedMixHeadroomGain:F4} active=[{activeVoiceFrequencies}]");
            }

            if (completedVoiceDiagnostics is not null)
            {
                foreach (var completedVoice in completedVoiceDiagnostics)
                {
                    _pendingVoiceCompletionLogs.Add(new PendingVoiceCompletionLog(
                        completedVoice.Frequency,
                        completedVoice.LevelBeforeRemoval,
                        mixGainBeforeRemoval,
                        mixGainAfterRemoval,
                        remainingVoiceCount,
                        activeVoiceFrequencies,
                        _recentSamples.ToArray()));
                }
            }
        }

        _previousSample = outputSample;
        _sampleFrameIndex++;
        return outputSample;
    }

    private float CreateWaveSample(double phase)
    {
        return _waveform switch
        {
            OscillatorWaveform.Sine => (float)Math.Sin(phase * TwoPi),
            OscillatorWaveform.Triangle => 1.0f - (4.0f * MathF.Abs((float)phase - 0.5f)),
            OscillatorWaveform.TriSaw => CreateTriSawSample((float)phase),
            OscillatorWaveform.Square => phase < 0.5 ? 1.0f : -1.0f,
            _ => (float)((2.0 * phase) - 1.0)
        };
    }

    private float ApplyBitReduction(float sample)
    {
        if (_bitRedux == 0)
        {
            return sample;
        }

        var steps = 1 << (_bitRedux - 1);
        return MathF.Round(sample * steps) / steps;
    }

    private float CalculateEffectiveFrequency(float baseFrequency)
    {
        var noteSemitoneOffset = 12.0 * Math.Log2(baseFrequency / BaseKeytrackFrequency);
        var trackedFrequency = BaseKeytrackFrequency * Math.Pow(2.0, (noteSemitoneOffset * _keytrackPercent) / 1200.0);
        var tuningOffset = _semiOffset + (_centsOffset / 100.0);
        return (float)(trackedFrequency * Math.Pow(2.0, tuningOffset / 12.0));
    }

    private float ApplyFilter(float sample, bool hasNoActiveVoices)
    {
        var cutoffHz = ResolveFilterCutoffFrequency();

        if (_filterType == FilterType.Formant)
        {
            // For the Formant filter, Cutoff serves directly as the Vowel morph interpolation (0-128)
            var filteredOutput = _ladderFilter!.ProcessFormant(sample, _filterCutoff, _filterResonance, FormantVowOrder, FormantControl);

            if (hasNoActiveVoices && MathF.Abs((float)filteredOutput) < 0.000001f)
            {
                ResetFilterState();
            }

            return (float)filteredOutput;
        }

        if (_filterCutoff >= 128.0f && _filterResonance <= 0.0f && _filterDrive <= 0.0f)
        {
            if (hasNoActiveVoices)
            {
                ResetFilterState();
            }

            return sample;
        }

        var drivenInput = _filterDriveRoute == FilterDriveRoute.Pre
            ? ApplyDrive(sample)
            : sample;

        var normalFilteredOutput = _ladderFilter!.Process(drivenInput, cutoffHz, _filterResonance, _filterType);

        if (hasNoActiveVoices && MathF.Abs((float)normalFilteredOutput) < 0.000001f)
        {
            ResetFilterState();
        }

        return _filterDriveRoute == FilterDriveRoute.Post
            ? ApplyDrive((float)normalFilteredOutput)
            : (float)normalFilteredOutput;
    }

    private static float CreateTriSawSample(float phase)
    {
        var triangle = 1.0f - (4.0f * MathF.Abs(phase - 0.5f));
        var saw = (2.0f * phase) - 1.0f;
        return (triangle + saw) * 0.5f;
    }

    private int GetReleaseSampleCount() => Math.Max(1, (_waveFormat.SampleRate * ReleaseDurationMilliseconds) / 1000);

    private int GetAttackSampleCount() => Math.Max(1, (_waveFormat.SampleRate * AttackDurationMilliseconds) / 1000);

    private float ApplyDrive(float sample)
    {
        if (_filterDrive <= 0.0f)
        {
            return sample;
        }

        var driveAmount = 1.0f + ((_filterDrive / 128.0f) * 7.0f);
        return MathF.Tanh(sample * driveAmount) / MathF.Tanh(driveAmount);
    }

    private float GetTrackedFilterFrequency()
    {
        if (_voices.Count == 0)
        {
            return _lastTrackedFilterFrequency;
        }

        if (_voices.ContainsKey(_lastTrackedFilterFrequency))
        {
            return _lastTrackedFilterFrequency;
        }

        return _voices.Keys.Last();
    }

    private void ResetFilterState()
    {
        _ladderFilter?.Reset();
        // Do not reset _lastTrackedFilterFrequency to BaseKeytrackFrequency here,
        // so the filter envelope/cutoff doesn't instantly jump when voices finish.
    }





    // Using an exponential scale map creates a much smoother, perceptually linear sweep.
    private float ResolveFilterCutoffFrequency()
    {
        // Smoothly map 0-128 UI values into 0.0-1.0 to handle standard synth semantics nicely
        var normalizedCutoff = MathF.Max(0.0f, _filterCutoff / 128.0f);
        var maximumCutoffHz = _waveFormat.SampleRate * 0.49f;

        // Convert knob position to Hz using exponential mapping first to make linear UI
        // feel physically continuous across frequency
        var baseCutoffHz = MinimumFilterCutoffFrequency * MathF.Pow(maximumCutoffHz / MinimumFilterCutoffFrequency, normalizedCutoff);

        // Apply keytracking as a semitone shift in the frequency domain.
        // 100% means cutoff tracks pitch 1:1 in semitones relative to the reference pitch (A4).
        var noteSemitoneOffset = 12.0 * Math.Log2(GetTrackedFilterFrequency() / BaseKeytrackFrequency);
        var trackedCutoffHz = baseCutoffHz * Math.Pow(2.0, (noteSemitoneOffset * _filterKeytrackPercent) / 1200.0);

        return Math.Clamp((float)trackedCutoffHz, MinimumFilterCutoffFrequency, maximumCutoffHz);
    }

    private void CaptureRecentSample(SampleSnapshot sampleSnapshot)
    {
        _recentSamples.Enqueue(sampleSnapshot);

        while (_recentSamples.Count > DiagnosticWindowSampleCount)
        {
            _recentSamples.Dequeue();
        }
    }

    private void FlushPendingVoiceCompletionLogs(SampleSnapshot sampleSnapshot)
    {
        if (!_isOscillatorLoggingEnabled || _pendingVoiceCompletionLogs.Count == 0)
        {
            return;
        }

        for (var index = _pendingVoiceCompletionLogs.Count - 1; index >= 0; index--)
        {
            var pendingLog = _pendingVoiceCompletionLogs[index];
            pendingLog.CapturePostRemovalSample(sampleSnapshot);

            if (!pendingLog.IsReadyToWrite)
            {
                continue;
            }

            Log($"VoiceCompleted freq={pendingLog.Frequency:F2} frame={pendingLog.PreRemovalSamples[^1].FrameIndex} levelBeforeRemoval={pendingLog.LevelBeforeRemoval:F4} voicesAfter={pendingLog.ActiveVoiceCountAfterRemoval} gainBefore={pendingLog.MixGainBeforeRemoval:F4} gainAfter={pendingLog.MixGainAfterRemoval:F4} activeAfter=[{pendingLog.ActiveVoiceFrequenciesAfterRemoval}]");
            Log($"VoiceCompletedWindow pre=[{FormatSampleWindow(pendingLog.PreRemovalSamples)}] post=[{FormatSampleWindow(pendingLog.PostRemovalSamples)}]");
            _pendingVoiceCompletionLogs.RemoveAt(index);
        }
    }

    private string FormatActiveVoiceFrequencies()
    {
        return string.Join(", ", _voices.Keys.OrderBy(frequency => frequency).Select(frequency => frequency.ToString("F2")));
    }

    private static string FormatSampleWindow(IEnumerable<SampleSnapshot> sampleWindow)
    {
        return string.Join(", ", sampleWindow.Select(sample => $"#{sample.FrameIndex}:s={sample.Sample:F4}:d={sample.Delta:F4}:v={sample.ActiveVoiceCount}:g={sample.MixGain:F4}"));
    }

    private void Log(string message)
    {
        if (_isOscillatorLoggingEnabled)
        {
            Debug.WriteLine($"[Oscillator] {message}");
        }
    }

    private sealed record CompletedVoiceDiagnostic(float Frequency, float LevelBeforeRemoval);

    private sealed record SampleSnapshot(long FrameIndex, float Sample, float Delta, int ActiveVoiceCount, float MixGain);

    private sealed class PendingVoiceCompletionLog(
        float frequency,
        float levelBeforeRemoval,
        float mixGainBeforeRemoval,
        float mixGainAfterRemoval,
        int activeVoiceCountAfterRemoval,
        string activeVoiceFrequenciesAfterRemoval,
        IReadOnlyList<SampleSnapshot> preRemovalSamples)
    {
        public float Frequency { get; } = frequency;
        public float LevelBeforeRemoval { get; } = levelBeforeRemoval;
        public float MixGainBeforeRemoval { get; } = mixGainBeforeRemoval;
        public float MixGainAfterRemoval { get; } = mixGainAfterRemoval;
        public int ActiveVoiceCountAfterRemoval { get; } = activeVoiceCountAfterRemoval;
        public string ActiveVoiceFrequenciesAfterRemoval { get; } = activeVoiceFrequenciesAfterRemoval;
        public IReadOnlyList<SampleSnapshot> PreRemovalSamples { get; } = preRemovalSamples;
        public List<SampleSnapshot> PostRemovalSamples { get; } = [];
        public bool IsReadyToWrite => PostRemovalSamples.Count >= DiagnosticWindowSampleCount;

        public void CapturePostRemovalSample(SampleSnapshot sampleSnapshot)
        {
            if (!IsReadyToWrite)
            {
                PostRemovalSamples.Add(sampleSnapshot);
            }
        }
    }

    private sealed class VoiceState(float baseFrequency, int attackSampleCount)
    {
        private readonly int _attackSampleCount = Math.Max(1, attackSampleCount);
        private int _releaseSampleCount;
        private int _attackSamplesRemaining = Math.Max(1, attackSampleCount);

        public float BaseFrequency { get; } = baseFrequency;
        public double Phase { get; set; }
        public bool IsReleasing { get; private set; }
        public float Level { get; private set; }
        public int ReleaseSamplesRemaining { get; private set; }

        public void BeginRelease(int releaseSampleCount)
        {
            if (IsReleasing)
            {
                return;
            }

            IsReleasing = true;
            _releaseSampleCount = Math.Max(1, releaseSampleCount);
            ReleaseSamplesRemaining = _releaseSampleCount;
        }

        public void AdvanceAttack()
        {
            if (IsReleasing || _attackSamplesRemaining <= 0)
            {
                Level = 1.0f;
                return;
            }

            var normalizedTime = (float)(_attackSampleCount - _attackSamplesRemaining) / _attackSampleCount;
            Level = MathF.Sin(normalizedTime * (MathF.PI / 2.0f));
            _attackSamplesRemaining--;

            if (_attackSamplesRemaining <= 0)
            {
                Level = 1.0f;
            }
        }

        public void AdvanceRelease()
        {
            if (!IsReleasing)
            {
                return;
            }

            ReleaseSamplesRemaining--;
            if (ReleaseSamplesRemaining <= 0)
            {
                Level = 0.0f;
                return;
            }

            var normalizedTime = (float)(_releaseSampleCount - ReleaseSamplesRemaining) / _releaseSampleCount;
            Level = MathF.Cos(normalizedTime * (MathF.PI / 2.0f));
        }
    }
}
