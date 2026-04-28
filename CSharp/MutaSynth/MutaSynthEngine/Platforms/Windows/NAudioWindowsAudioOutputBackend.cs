using System.Collections.Immutable;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace MutaSynthEngine.Platforms.Windows;

public sealed class NAudioWindowsAudioOutputBackend : IDisposable
{
    private const int ChannelCount = 2;

    private int _sampleRate;
    private int _bufferSize;
    private WasapiOut? _output;
    private LiveOscillatorSampleProvider? _sampleProvider;
    private string? _selectedDeviceId;
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

    public NAudioWindowsAudioOutputBackend(int sampleRate, int bufferSize, float volume)
    {
        _sampleRate = sampleRate;
        _bufferSize = bufferSize;
        _volume = volume;
    }

    public string? SelectedDeviceId => _selectedDeviceId;

    public float Volume
    {
        get => _volume;
        set
        {
            _volume = value;

            if (_sampleProvider is not null)
            {
                _sampleProvider.Volume = value;
            }
        }
    }

    public ImmutableArray<AudioDriverDeviceOption> GetDevices()
    {
        using var enumerator = new MMDeviceEnumerator();
        var collection = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

        return collection
            .Select(device => new AudioDriverDeviceOption(device.ID, device.FriendlyName))
            .ToImmutableArray();
    }

    public void SelectDevice(string? deviceId)
    {
        if (_selectedDeviceId == deviceId)
        {
            return;
        }

        _selectedDeviceId = deviceId;
        RecreatePlayback();
    }

    public void UpdateAudioSettings(int sampleRate, int bufferSize)
    {
        if (_sampleRate == sampleRate && _bufferSize == bufferSize)
        {
            return;
        }

        _sampleRate = sampleRate;
        _bufferSize = bufferSize;
        RecreatePlayback();
    }

    public void UpdateOscillatorSettings(OscillatorWaveform waveform, int semiOffset, int centsOffset, int bitRedux, int keytrackPercent, bool isOscillatorLoggingEnabled)
    {
        _waveform = waveform;
        _semiOffset = semiOffset;
        _centsOffset = centsOffset;
        _bitRedux = bitRedux;
        _keytrackPercent = keytrackPercent;
        _isOscillatorLoggingEnabled = isOscillatorLoggingEnabled;

        _sampleProvider?.SetOscillatorParameters(waveform, semiOffset, centsOffset, bitRedux, keytrackPercent, isOscillatorLoggingEnabled);
    }

    public void UpdateFilterSettings(FilterType filterType, float cutoff, float resonance, int keytrackPercent, float drive, FilterDriveRoute driveRoute)
    {
        _filterType = filterType;
        _filterCutoff = cutoff;
        _filterResonance = resonance;
        _filterKeytrackPercent = keytrackPercent;
        _filterDrive = drive;
        _filterDriveRoute = driveRoute;

        _sampleProvider?.SetFilterParameters(filterType, cutoff, resonance, keytrackPercent, drive, driveRoute);
    }

    public void EnsureReady()
    {
        if (_output is not null && _sampleProvider is not null)
        {
            return;
        }

        using var enumerator = new MMDeviceEnumerator();
        using var defaultDevice = ResolveDevice(enumerator, _selectedDeviceId);

        _sampleProvider = new LiveOscillatorSampleProvider(_sampleRate, ChannelCount, _volume);
        _sampleProvider.SetOscillatorParameters(_waveform, _semiOffset, _centsOffset, _bitRedux, _keytrackPercent, _isOscillatorLoggingEnabled);
        _sampleProvider.SetFilterParameters(_filterType, _filterCutoff, _filterResonance, _filterKeytrackPercent, _filterDrive, _filterDriveRoute);
        _output = new WasapiOut(defaultDevice, AudioClientShareMode.Shared, false, _bufferSize);
        _output.Init(_sampleProvider);
        _output.Play();
    }

    public void NoteOn(float frequency)
    {
        EnsureReady();
        _sampleProvider?.NoteOn(frequency);
    }

    public void NoteOff(float frequency)
    {
        _sampleProvider?.NoteOff(frequency);
    }

    public void NoteOff() => _sampleProvider?.NoteOff();

    public void Dispose() => RecreatePlayback();

    private void RecreatePlayback()
    {
        _output?.Stop();
        _output?.Dispose();
        _output = null;
        _sampleProvider = null;
    }

    private static MMDevice ResolveDevice(MMDeviceEnumerator enumerator, string? deviceId)
    {
        if (!string.IsNullOrEmpty(deviceId))
        {
            return enumerator.GetDevice(deviceId);
        }

        return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
    }
}