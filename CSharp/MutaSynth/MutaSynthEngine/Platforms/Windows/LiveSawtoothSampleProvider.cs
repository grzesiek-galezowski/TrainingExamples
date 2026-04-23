using NAudio.Wave;

namespace MutaSynthEngine.Platforms.Windows;

public sealed class LiveSawtoothSampleProvider : ISampleProvider
{
    private readonly WaveFormat _waveFormat;
    private readonly object _sync = new();
    private readonly Dictionary<float, VoiceState> _voices = [];
    private float _volume;

    public LiveSawtoothSampleProvider(int sampleRate, int channelCount, float volume)
    {
        _waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount);
        _volume = volume;
    }

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

    public void NoteOn(float frequency)
    {
        lock (_sync)
        {
            _voices[frequency] = new VoiceState(frequency);
        }
    }

    public void NoteOff()
    {
        lock (_sync)
        {
            _voices.Clear();
        }
    }

    public void NoteOff(float frequency)
    {
        lock (_sync)
        {
            _voices.Remove(frequency);
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
        if (_voices.Count == 0)
        {
            return 0.0f;
        }

        var mixedSample = 0.0f;

        foreach (var voice in _voices.Values)
        {
            var sample = (float)(2.0 * voice.Phase - 1.0);
            mixedSample += sample;

            voice.Phase += voice.Frequency / _waveFormat.SampleRate;
            if (voice.Phase >= 1.0)
            {
                voice.Phase -= Math.Floor(voice.Phase);
            }
        }

        return mixedSample / _voices.Count * _volume;
    }

    private sealed class VoiceState(float frequency)
    {
        public float Frequency { get; } = frequency;
        public double Phase { get; set; }
    }
}