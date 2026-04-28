using System.Collections.Immutable;

namespace MutaSynthEngine;

/// <summary>
/// No-op driver used on platforms where audio is not yet implemented.
/// </summary>
public sealed class NullAudioDriver : IAudioDriver
{
    public ImmutableArray<AudioDriverDeviceOption> MidiInputDevices { get; } = [];
    public ImmutableArray<AudioDriverDeviceOption> AudioOutputDevices { get; } = [];
    public ImmutableArray<NotePlaybackMode> NotePlaybackModes { get; } = [NotePlaybackMode.Monophonic, NotePlaybackMode.Polyphonic];
    public ImmutableArray<OscillatorWaveform> Waveforms { get; } = [OscillatorWaveform.Sine, OscillatorWaveform.Triangle, OscillatorWaveform.TriSaw, OscillatorWaveform.Saw, OscillatorWaveform.Square];
    public ImmutableArray<FilterType> FilterTypes { get; } = [FilterType.LpLdr12, FilterType.LpLdr14, FilterType.LpFat12, FilterType.LpFat14];
    public ImmutableArray<FilterDriveRoute> FilterDriveRoutes { get; } = [FilterDriveRoute.Pre, FilterDriveRoute.Post];
    public ImmutableArray<int> MidiChannels { get; } = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];
    public ImmutableArray<int> SampleRates { get; } = [22_050, 44_100, 48_000, 96_000];
    public ImmutableArray<int> BufferSizes { get; } = [64, 128, 256, 512, 1024];
    public ImmutableArray<int> BitReduxLevels { get; } = [0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 16];
    public string? SelectedMidiInputDeviceId { get; private set; }
    public string? SelectedAudioOutputDeviceId { get; private set; }
    public NotePlaybackMode SelectedNotePlaybackMode { get; private set; } = NotePlaybackMode.Monophonic;
    public OscillatorWaveform SelectedWaveform { get; private set; } = OscillatorWaveform.Saw;
    public int SelectedMidiChannel { get; private set; } = 1;
    public int SelectedSampleRate { get; private set; } = 48_000;
    public int SelectedBufferSize { get; private set; } = 512;
    public int SelectedSemiOffset { get; private set; }
    public int SelectedCentsOffset { get; private set; }
    public int SelectedBitRedux { get; private set; }
    public int SelectedKeytrackPercent { get; private set; } = 100;
    public FilterType SelectedFilterType { get; private set; } = FilterType.LpLdr12;
    public float SelectedFilterCutoff { get; private set; } = 128.0f;
    public float SelectedFilterResonance { get; private set; }
    public int SelectedFilterKeytrackPercent { get; private set; } = 100;
    public float SelectedFilterDrive { get; private set; }
    public FilterDriveRoute SelectedFilterDriveRoute { get; private set; } = FilterDriveRoute.Pre;
    public bool IsOscillatorLoggingEnabled { get; private set; }
    public float Volume { get; set; } = 0.5f;
    public Task InitializeAsync() => Task.CompletedTask;
    public void SelectAudioOutputDevice(string? deviceId) => SelectedAudioOutputDeviceId = deviceId;
    public void SelectBitRedux(int bitRedux)
    {
        if (BitReduxLevels.Contains(bitRedux))
        {
            SelectedBitRedux = bitRedux;
        }
    }

    public void SelectCentsOffset(int centsOffset)
    {
        if (centsOffset is >= -50 and <= 50)
        {
            SelectedCentsOffset = centsOffset;
        }
    }

    public void SelectFilterCutoff(float cutoff)
    {
        if (cutoff is >= 0.0f and <= 128.0f)
        {
            SelectedFilterCutoff = cutoff;
        }
    }

    public void SelectFilterDrive(float drive)
    {
        if (drive is >= 0.0f and <= 128.0f)
        {
            SelectedFilterDrive = drive;
        }
    }

    public void SelectFilterDriveRoute(FilterDriveRoute driveRoute)
    {
        if (FilterDriveRoutes.Contains(driveRoute))
        {
            SelectedFilterDriveRoute = driveRoute;
        }
    }

    public void SelectFilterKeytrackPercent(int keytrackPercent)
    {
        if (keytrackPercent is >= -200 and <= 200)
        {
            SelectedFilterKeytrackPercent = keytrackPercent;
        }
    }

    public void SelectFilterResonance(float resonance)
    {
        if (resonance is >= 0.0f and <= 128.0f)
        {
            SelectedFilterResonance = resonance;
        }
    }

    public void SelectFilterType(FilterType filterType)
    {
        if (FilterTypes.Contains(filterType))
        {
            SelectedFilterType = filterType;
        }
    }

    public void SelectKeytrackPercent(int keytrackPercent)
    {
        if (keytrackPercent is >= 0 and <= 200)
        {
            SelectedKeytrackPercent = keytrackPercent;
        }
    }

    public void SelectNotePlaybackMode(NotePlaybackMode notePlaybackMode) => SelectedNotePlaybackMode = notePlaybackMode;
    public void SelectBufferSize(int bufferSize) => SelectedBufferSize = bufferSize;
    public void SelectMidiChannel(int midiChannel) => SelectedMidiChannel = midiChannel;
    public void SelectMidiInputDevice(string? deviceId) => SelectedMidiInputDeviceId = deviceId;
    public void SelectSampleRate(int sampleRate) => SelectedSampleRate = sampleRate;
    public void SelectSemiOffset(int semiOffset)
    {
        if (semiOffset is >= -36 and <= 36)
        {
            SelectedSemiOffset = semiOffset;
        }
    }

    public void SelectOscillatorLogging(bool isEnabled) => IsOscillatorLoggingEnabled = isEnabled;

    public void SelectWaveform(OscillatorWaveform waveform)
    {
        if (Waveforms.Contains(waveform))
        {
            SelectedWaveform = waveform;
        }
    }

    public void NoteOn(float frequency) { }
    public void NoteOff() { }
    public string RunAudioTest() => "Audio test is not available on this platform.";
    public string RunMidiTest() => "MIDI test is not available on this platform.";
    public void Dispose() { }
}
