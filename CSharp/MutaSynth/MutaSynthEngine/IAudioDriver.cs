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
    FilterType SelectedFilterType { get; }
    float SelectedFilterCutoff { get; }
    float SelectedFilterResonance { get; }
    int SelectedFilterKeytrackPercent { get; }
    float SelectedFilterDrive { get; }
    FilterDriveRoute SelectedFilterDriveRoute { get; }
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
    void SelectKeytrackPercent(int keytrackPercent);
    void SelectNotePlaybackMode(NotePlaybackMode notePlaybackMode);
    void SelectBufferSize(int bufferSize);
    void SelectMidiChannel(int midiChannel);
    void SelectMidiInputDevice(string? deviceId);
    void SelectSampleRate(int sampleRate);
    void SelectSemiOffset(int semiOffset);
    void SelectOscillatorLogging(bool isEnabled);
    void SelectWaveform(OscillatorWaveform waveform);
    void NoteOn(float frequency);
    void NoteOff();
    string RunAudioTest();
    string RunMidiTest();
}
