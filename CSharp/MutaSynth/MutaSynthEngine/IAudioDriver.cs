using System.Collections.Immutable;

namespace MutaSynthEngine;

public interface IAudioDriver : IDisposable
{
    ImmutableArray<AudioDriverDeviceOption> MidiInputDevices { get; }
    ImmutableArray<AudioDriverDeviceOption> AudioOutputDevices { get; }
    ImmutableArray<NotePlaybackMode> NotePlaybackModes { get; }
    ImmutableArray<OscillatorWaveform> Waveforms { get; }
    ImmutableArray<FilterType> FilterTypes { get; }
    ImmutableArray<FilterDriveRoute> FilterDriveRoutes { get; }
    ImmutableArray<int> MidiChannels { get; }
    ImmutableArray<int> SampleRates { get; }
    ImmutableArray<int> BufferSizes { get; }
    ImmutableArray<int> BitReduxLevels { get; }
    string? SelectedMidiInputDeviceId { get; }
    string? SelectedAudioOutputDeviceId { get; }
    NotePlaybackMode SelectedNotePlaybackMode { get; }
    OscillatorWaveform SelectedWaveform { get; }
    int SelectedMidiChannel { get; }
    int SelectedSampleRate { get; }
    int SelectedBufferSize { get; }
    int SelectedSemiOffset { get; }
    int SelectedCentsOffset { get; }
    int SelectedBitRedux { get; }
    int SelectedKeytrackPercent { get; }

    int SelectedOsc2SemiOffset { get; }
    int SelectedOsc2CentsOffset { get; }
    OscillatorWaveform SelectedOsc2Waveform { get; }
    int SelectedOsc2BitRedux { get; }
    int SelectedOsc2KeytrackPercent { get; }

    int SelectedOsc3SemiOffset { get; }
    int SelectedOsc3CentsOffset { get; }
    OscillatorWaveform SelectedOsc3Waveform { get; }
    int SelectedOsc3BitRedux { get; }
    int SelectedOsc3KeytrackPercent { get; }

    float SelectedOsc1Volume { get; }
    float SelectedOsc2Volume { get; }
    float SelectedOsc3Volume { get; }

    FilterType SelectedFilterType { get; }
    float SelectedFilterCutoff { get; }
    float SelectedFilterResonance { get; }
    int SelectedFilterKeytrackPercent { get; }
    float SelectedFilterDrive { get; }
    FilterDriveRoute SelectedFilterDriveRoute { get; }
    int SelectedFormantVowOrder { get; }
    float SelectedFormantControl { get; }
    bool IsOscillatorLoggingEnabled { get; }
    float Volume { get; set; }
    Task InitializeAsync();
    void SelectAudioOutputDevice(string? deviceId);
    void SelectBitRedux(int bitRedux);
    void SelectCentsOffset(int centsOffset);
    void SelectFilterCutoff(float cutoff);
    void SelectFilterDrive(float drive);
    void SelectFilterDriveRoute(FilterDriveRoute driveRoute);
    void SelectFilterKeytrackPercent(int keytrackPercent);
    void SelectFilterResonance(float resonance);
    void SelectFilterType(FilterType filterType);
    void SelectFormantVowOrder(int order);
    void SelectFormantControl(float control);
    void SelectKeytrackPercent(int keytrackPercent);
    void SelectNotePlaybackMode(NotePlaybackMode notePlaybackMode);
    void SelectBufferSize(int bufferSize);
    void SelectMidiChannel(int midiChannel);
    void SelectMidiInputDevice(string? deviceId);
    void SelectSampleRate(int sampleRate);
    void SelectSemiOffset(int semiOffset);
    void SelectOscillatorLogging(bool isEnabled);
    void SelectWaveform(OscillatorWaveform waveform);

    void SelectOsc2SemiOffset(int semiOffset);
    void SelectOsc2CentsOffset(int centsOffset);
    void SelectOsc2Waveform(OscillatorWaveform waveform);
    void SelectOsc2BitRedux(int bitRedux);
    void SelectOsc2KeytrackPercent(int keytrackPercent);

    void SelectOsc3SemiOffset(int semiOffset);
    void SelectOsc3CentsOffset(int centsOffset);
    void SelectOsc3Waveform(OscillatorWaveform waveform);
    void SelectOsc3BitRedux(int bitRedux);
    void SelectOsc3KeytrackPercent(int keytrackPercent);

    void SelectOsc1Volume(float volume);
    void SelectOsc2Volume(float volume);
    void SelectOsc3Volume(float volume);

    void NoteOn(float frequency);
    void NoteOff();
    string RunAudioTest();
    string RunMidiTest();
}
