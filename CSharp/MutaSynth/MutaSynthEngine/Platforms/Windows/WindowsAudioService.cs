using System.Collections.Immutable;
using System.Diagnostics;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;

namespace MutaSynthEngine.Platforms.Windows;

/// <summary>
/// Windows audio driver with selectable MIDI input, MIDI channel, and audio output device.
/// </summary>
public sealed class WindowsAudioService : IAudioDriver
{
    private static readonly ImmutableArray<int> AvailableMidiChannels = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];
    private static readonly ImmutableArray<int> AvailableSampleRates = [22_050, 44_100, 48_000, 96_000];
    private static readonly ImmutableArray<int> AvailableBufferSizes = [64, 128, 256, 512, 1024];
    private static readonly ImmutableArray<NotePlaybackMode> AvailableNotePlaybackModes = [NotePlaybackMode.Monophonic, NotePlaybackMode.Polyphonic];
    private static readonly ImmutableArray<OscillatorWaveform> AvailableWaveforms = [OscillatorWaveform.Sine, OscillatorWaveform.Triangle, OscillatorWaveform.TriSaw, OscillatorWaveform.Saw, OscillatorWaveform.Square];
    private static readonly ImmutableArray<FilterType> AvailableFilterTypes = [FilterType.LpLdr12, FilterType.LpLdr14, FilterType.LpFat12, FilterType.LpFat14, FilterType.HpSqu24, FilterType.Formant];
    private static readonly ImmutableArray<FilterDriveRoute> AvailableFilterDriveRoutes = [FilterDriveRoute.Pre, FilterDriveRoute.Post];
    private static readonly ImmutableArray<int> AvailableBitReduxLevels = [0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 16];
    private const int TestNoteDurationMilliseconds = 750;
    private const float TestMidiFrequency = 440.0f;
    private const float TestAudioFrequency = 220.0f;

    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private readonly NAudioWindowsAudioOutputBackend _audioOutputBackend;
    private InputDevice? _midiInputDevice;
    private bool _disposed;
    private int _selectedSampleRate = 44_100;
    private int _selectedBufferSize = 64;
    private NotePlaybackMode _selectedNotePlaybackMode = NotePlaybackMode.Monophonic;
    private OscillatorWaveform _selectedWaveform = OscillatorWaveform.Saw;
    private int _selectedSemiOffset;
    private int _selectedCentsOffset;
    private int _selectedBitRedux;
    private int _selectedKeytrackPercent = 100;
    private FilterType _selectedFilterType = FilterType.LpLdr12;
    private float _selectedFilterCutoff = 128.0f;
    private float _selectedFilterResonance;
    private int _selectedFilterKeytrackPercent = 100;
    private float _selectedFilterDrive;
    private FilterDriveRoute _selectedFilterDriveRoute = FilterDriveRoute.Pre;
    private bool _isOscillatorLoggingEnabled;
    private float _volume = 0.5f;
    private CancellationTokenSource? _testPlaybackCancellation;
    private readonly List<float> _activeFrequencies = [];

    public WindowsAudioService()
    {
        _audioOutputBackend = new NAudioWindowsAudioOutputBackend(_selectedSampleRate, _selectedBufferSize, _volume);
    }

    public ImmutableArray<AudioDriverDeviceOption> MidiInputDevices { get; private set; } = [];
    public ImmutableArray<AudioDriverDeviceOption> AudioOutputDevices { get; private set; } = [];
    public ImmutableArray<NotePlaybackMode> NotePlaybackModes => AvailableNotePlaybackModes;
    public ImmutableArray<OscillatorWaveform> Waveforms => AvailableWaveforms;
    public ImmutableArray<FilterType> FilterTypes => AvailableFilterTypes;
    public ImmutableArray<FilterDriveRoute> FilterDriveRoutes => AvailableFilterDriveRoutes;
    public ImmutableArray<int> MidiChannels => AvailableMidiChannels;
    public ImmutableArray<int> SampleRates => AvailableSampleRates;
    public ImmutableArray<int> BufferSizes => AvailableBufferSizes;
    public ImmutableArray<int> BitReduxLevels => AvailableBitReduxLevels;
    public string? SelectedMidiInputDeviceId { get; private set; }
    public string? SelectedAudioOutputDeviceId { get; private set; }
    public NotePlaybackMode SelectedNotePlaybackMode => _selectedNotePlaybackMode;
    public OscillatorWaveform SelectedWaveform => _selectedWaveform;
    public int SelectedMidiChannel { get; private set; } = 1;
    public int SelectedSampleRate => _selectedSampleRate;
    public int SelectedBufferSize => _selectedBufferSize;
    public int SelectedSemiOffset => _selectedSemiOffset;
    public int SelectedCentsOffset => _selectedCentsOffset;
    public int SelectedBitRedux => _selectedBitRedux;
    public int SelectedKeytrackPercent => _selectedKeytrackPercent;
    public FilterType SelectedFilterType => _selectedFilterType;
    public float SelectedFilterCutoff => _selectedFilterCutoff;
    public float SelectedFilterResonance => _selectedFilterResonance;
    public int SelectedFilterKeytrackPercent => _selectedFilterKeytrackPercent;
    public float SelectedFilterDrive => _selectedFilterDrive;
    public FilterDriveRoute SelectedFilterDriveRoute => _selectedFilterDriveRoute;
    public int SelectedFormantVowOrder => _audioOutputBackend.SelectedFormantVowOrder;
    public float SelectedFormantControl => _audioOutputBackend.SelectedFormantControl;
    public bool IsOscillatorLoggingEnabled => _isOscillatorLoggingEnabled;

    public float Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            _audioOutputBackend.Volume = value;
        }
    }

    public void SelectOscillatorLogging(bool isEnabled)
    {
        ThrowIfDisposed();

        if (_isOscillatorLoggingEnabled == isEnabled)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _isOscillatorLoggingEnabled = isEnabled;
            ApplyOscillatorSettings();
            Debug.WriteLine($"[Oscillator] Logging {(isEnabled ? "enabled" : "disabled")}");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectBitRedux(int bitRedux)
    {
        ThrowIfDisposed();

        if (!AvailableBitReduxLevels.Contains(bitRedux) || _selectedBitRedux == bitRedux)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedBitRedux = bitRedux;
            ApplyOscillatorSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectCentsOffset(int centsOffset)
    {
        ThrowIfDisposed();

        if (centsOffset is < -50 or > 50 || _selectedCentsOffset == centsOffset)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedCentsOffset = centsOffset;
            ApplyOscillatorSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectFilterCutoff(float cutoff)
    {
        ThrowIfDisposed();

        if (cutoff is < 0.0f or > 128.0f || _selectedFilterCutoff == cutoff)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedFilterCutoff = cutoff;
            ApplyFilterSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectFilterDrive(float drive)
    {
        ThrowIfDisposed();

        if (drive is < 0.0f or > 128.0f || _selectedFilterDrive == drive)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedFilterDrive = drive;
            ApplyFilterSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectFilterDriveRoute(FilterDriveRoute driveRoute)
    {
        ThrowIfDisposed();

        if (!AvailableFilterDriveRoutes.Contains(driveRoute) || _selectedFilterDriveRoute == driveRoute)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedFilterDriveRoute = driveRoute;
            ApplyFilterSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectFilterKeytrackPercent(int keytrackPercent)
    {
        ThrowIfDisposed();

        if (keytrackPercent is < -200 or > 200 || _selectedFilterKeytrackPercent == keytrackPercent)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedFilterKeytrackPercent = keytrackPercent;
            ApplyFilterSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectFilterResonance(float resonance)
    {
        ThrowIfDisposed();

        if (resonance is < 0.0f or > 128.0f || _selectedFilterResonance == resonance)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedFilterResonance = resonance;
            ApplyFilterSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectFilterType(FilterType filterType)
    {
        ThrowIfDisposed();

        if (!AvailableFilterTypes.Contains(filterType) || _selectedFilterType == filterType)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedFilterType = filterType;
            ApplyFilterSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectKeytrackPercent(int keytrackPercent)
    {
        ThrowIfDisposed();

        if (keytrackPercent is < 0 or > 200 || _selectedKeytrackPercent == keytrackPercent)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedKeytrackPercent = keytrackPercent;
            ApplyOscillatorSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectFormantVowOrder(int order)
    {
        ThrowIfDisposed();

        if (order is < 0 or > 7 || _audioOutputBackend.SelectedFormantVowOrder == order)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _audioOutputBackend.SelectedFormantVowOrder = order;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectFormantControl(float control)
    {
        ThrowIfDisposed();

        if (control is < 0.0f or > 128.0f || _audioOutputBackend.SelectedFormantControl == control)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _audioOutputBackend.SelectedFormantControl = control;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectNotePlaybackMode(NotePlaybackMode notePlaybackMode)
    {
        ThrowIfDisposed();

        if (!AvailableNotePlaybackModes.Contains(notePlaybackMode) || _selectedNotePlaybackMode == notePlaybackMode)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedNotePlaybackMode = notePlaybackMode;
            StopPlayback();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectSemiOffset(int semiOffset)
    {
        ThrowIfDisposed();

        if (semiOffset is < -36 or > 36 || _selectedSemiOffset == semiOffset)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedSemiOffset = semiOffset;
            ApplyOscillatorSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectWaveform(OscillatorWaveform waveform)
    {
        ThrowIfDisposed();

        if (!AvailableWaveforms.Contains(waveform) || _selectedWaveform == waveform)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedWaveform = waveform;
            ApplyOscillatorSettings();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task InitializeAsync()
    {
        ThrowIfDisposed();

        await _semaphore.WaitAsync();
        try
        {
            RefreshAvailableDevices();
            OpenSelectedMidiInput();
            StopPlayback();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectAudioOutputDevice(string? deviceId)
    {
        ThrowIfDisposed();

        _semaphore.Wait();
        try
        {
            SelectedAudioOutputDeviceId = FindMatchingDeviceId(AudioOutputDevices, deviceId);
            _audioOutputBackend.SelectDevice(SelectedAudioOutputDeviceId);
            RefreshAvailableDevices();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectBufferSize(int bufferSize)
    {
        ThrowIfDisposed();

        if (!AvailableBufferSizes.Contains(bufferSize) || _selectedBufferSize == bufferSize)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedBufferSize = bufferSize;
            UpdateAudioSettings();
            RefreshAvailableDevices();
            OpenSelectedMidiInput();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectMidiChannel(int midiChannel)
    {
        ThrowIfDisposed();

        if (!AvailableMidiChannels.Contains(midiChannel))
        {
            return;
        }

        SelectedMidiChannel = midiChannel;
    }

    public void SelectMidiInputDevice(string? deviceId)
    {
        ThrowIfDisposed();

        _semaphore.Wait();
        try
        {
            SelectedMidiInputDeviceId = FindMatchingDeviceId(MidiInputDevices, deviceId);
            OpenSelectedMidiInput();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void SelectSampleRate(int sampleRate)
    {
        ThrowIfDisposed();

        if (!AvailableSampleRates.Contains(sampleRate) || _selectedSampleRate == sampleRate)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            _selectedSampleRate = sampleRate;
            UpdateAudioSettings();
            RefreshAvailableDevices();
            OpenSelectedMidiInput();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void NoteOn(float frequency)
    {
        ThrowIfDisposed();

        _semaphore.Wait();
        try
        {
            EnsurePlaybackReady();
            PlayFrequency(frequency);
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void NoteOff()
    {
        ThrowIfDisposed();

        _semaphore.Wait();
        try
        {
            StopPlayback();
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public string RunAudioTest()
    {
        ThrowIfDisposed();

        _semaphore.Wait();
        try
        {
            EnsurePlaybackReady();
            PlayTestNote(TestAudioFrequency, TimeSpan.FromMilliseconds(TestNoteDurationMilliseconds));

            var deviceName = AudioOutputDevices.FirstOrDefault(device => device.Id == SelectedAudioOutputDeviceId)?.Name ?? "default output";
            return $"Audio test played on {deviceName}.";
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public string RunMidiTest()
    {
        ThrowIfDisposed();

        _semaphore.Wait();
        try
        {
            EnsurePlaybackReady();
            PlayTestNote(TestMidiFrequency, TimeSpan.FromMilliseconds(TestNoteDurationMilliseconds));

            var midiName = MidiInputDevices.FirstOrDefault(device => device.Id == SelectedMidiInputDeviceId)?.Name ?? "manual test tone";
            return $"MIDI test triggered on channel {SelectedMidiChannel} using {midiName}.";
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _semaphore.Wait();
        try
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            CloseMidiInput();
            _testPlaybackCancellation?.Cancel();
            _testPlaybackCancellation?.Dispose();
            _testPlaybackCancellation = null;

            _audioOutputBackend.Dispose();
        }
        finally
        {
            _semaphore.Release();
            _semaphore.Dispose();
        }
    }

    private void RefreshAvailableDevices()
    {
        MidiInputDevices = InputDevice.GetAll()
            .Select(device => new AudioDriverDeviceOption(device.Name, device.Name))
            .ToImmutableArray();

        AudioOutputDevices = _audioOutputBackend.GetDevices();
        SelectedMidiInputDeviceId = FindMatchingDeviceId(MidiInputDevices, SelectedMidiInputDeviceId);

        // If no device is selected yet, default to the system default audio device
        if (SelectedAudioOutputDeviceId == null)
        {
            SelectedAudioOutputDeviceId = _audioOutputBackend.GetDefaultDeviceId();
        }

        SelectedAudioOutputDeviceId = FindMatchingDeviceId(AudioOutputDevices, SelectedAudioOutputDeviceId);
        _audioOutputBackend.SelectDevice(SelectedAudioOutputDeviceId);
    }

    private void OpenSelectedMidiInput()
    {
        CloseMidiInput();

        if (string.IsNullOrEmpty(SelectedMidiInputDeviceId))
        {
            return;
        }

        _midiInputDevice = InputDevice.GetAll().FirstOrDefault(device => device.Name == SelectedMidiInputDeviceId);
        if (_midiInputDevice is null)
        {
            return;
        }

        _midiInputDevice.EventReceived += OnMidiEventReceived;
        _midiInputDevice.StartEventsListening();
    }

    private void CloseMidiInput()
    {
        if (_midiInputDevice is null)
        {
            return;
        }

        _midiInputDevice.EventReceived -= OnMidiEventReceived;
        _midiInputDevice.StopEventsListening();
        _midiInputDevice.Dispose();
        _midiInputDevice = null;
    }

    private void OnMidiEventReceived(object? sender, MidiEventReceivedEventArgs e)
    {
        switch (e.Event)
        {
            case NoteOnEvent noteOnEvent when MatchesSelectedChannel(noteOnEvent.Channel) && noteOnEvent.Velocity > SevenBitNumber.MinValue:
                NoteOn(MidiNoteToFrequency(noteOnEvent.NoteNumber));
                break;

            case NoteOnEvent noteOnEvent when MatchesSelectedChannel(noteOnEvent.Channel):
                NoteOff(MidiNoteToFrequency(noteOnEvent.NoteNumber));
                break;

            case NoteOffEvent noteOffEvent when MatchesSelectedChannel(noteOffEvent.Channel):
                NoteOff(MidiNoteToFrequency(noteOffEvent.NoteNumber));
                break;
        }
    }

    private bool MatchesSelectedChannel(FourBitNumber channel) => channel + 1 == SelectedMidiChannel;

    private static float MidiNoteToFrequency(SevenBitNumber noteNumber) =>
        440.0f * MathF.Pow(2.0f, ((int)noteNumber - 69) / 12.0f);

    private void EnsurePlaybackReady()
    {
        _audioOutputBackend.EnsureReady();
    }

    private void PlayFrequency(float frequency)
    {
        _activeFrequencies.Remove(frequency);

        if (_selectedNotePlaybackMode == NotePlaybackMode.Monophonic)
        {
            _activeFrequencies.Add(frequency);
            _audioOutputBackend.NoteOff();
            _audioOutputBackend.NoteOn(frequency);
            return;
        }

        _activeFrequencies.Add(frequency);
        _audioOutputBackend.NoteOn(frequency);
    }

    private void NoteOff(float frequency)
    {
        var removed = _activeFrequencies.Remove(frequency);

        if (!removed)
        {
            return;
        }

        if (_selectedNotePlaybackMode == NotePlaybackMode.Polyphonic)
        {
            _audioOutputBackend.NoteOff(frequency);
            return;
        }

        if (_activeFrequencies.Count == 0)
        {
            _audioOutputBackend.NoteOff();
            return;
        }

        var frequencyToResume = _activeFrequencies[^1];
        _audioOutputBackend.NoteOff();
        _audioOutputBackend.NoteOn(frequencyToResume);
    }

    private void PlayTestNote(float frequency, TimeSpan duration)
    {
        _testPlaybackCancellation?.Cancel();
        _testPlaybackCancellation?.Dispose();
        _testPlaybackCancellation = new CancellationTokenSource();

        PlayFrequency(frequency);

        _ = Task.Run(async () =>
        {
            try
            {
                await Task.Delay(duration, _testPlaybackCancellation.Token);

                _semaphore.Wait();
                try
                {
                    if (_disposed)
                    {
                        return;
                    }

                    StopPlayback();
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            catch (TaskCanceledException)
            {
            }
            catch (ObjectDisposedException)
            {
            }
        }, _testPlaybackCancellation.Token);
    }

    private void StopPlayback()
    {
        _activeFrequencies.Clear();
        _audioOutputBackend.NoteOff();
    }

    private void UpdateAudioSettings()
    {
        _audioOutputBackend.UpdateAudioSettings(_selectedSampleRate, _selectedBufferSize);
        _audioOutputBackend.Volume = _volume;
        _audioOutputBackend.SelectDevice(SelectedAudioOutputDeviceId);
        ApplyOscillatorSettings();
        ApplyFilterSettings();
    }

    private void ApplyOscillatorSettings()
    {
        _audioOutputBackend.UpdateOscillatorSettings(_selectedWaveform, _selectedSemiOffset, _selectedCentsOffset, _selectedBitRedux, _selectedKeytrackPercent, _isOscillatorLoggingEnabled);
    }

    private void ApplyFilterSettings()
    {
        _audioOutputBackend.UpdateFilterSettings(_selectedFilterType, _selectedFilterCutoff, _selectedFilterResonance, _selectedFilterKeytrackPercent, _selectedFilterDrive, _selectedFilterDriveRoute);
    }

    private static string? FindMatchingDeviceId(ImmutableArray<AudioDriverDeviceOption> devices, string? deviceId)
    {
        if (devices.IsEmpty)
        {
            return null;
        }

        if (string.IsNullOrEmpty(deviceId))
        {
            return devices[0].Id;
        }

        return devices.Any(device => device.Id == deviceId)
            ? deviceId
            : devices[0].Id;
    }

    private void ThrowIfDisposed()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
    }
}