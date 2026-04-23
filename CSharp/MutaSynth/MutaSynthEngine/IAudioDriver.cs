using System.Collections.Immutable;

namespace MutaSynthEngine;

public interface IAudioDriver : IDisposable
{
    ImmutableArray<AudioDriverDeviceOption> MidiInputDevices { get; }
    ImmutableArray<AudioDriverDeviceOption> AudioOutputDevices { get; }
    ImmutableArray<NotePlaybackMode> NotePlaybackModes { get; }
    ImmutableArray<int> MidiChannels { get; }
    ImmutableArray<int> SampleRates { get; }
    ImmutableArray<int> BufferSizes { get; }
    string? SelectedMidiInputDeviceId { get; }
    string? SelectedAudioOutputDeviceId { get; }
    NotePlaybackMode SelectedNotePlaybackMode { get; }
    int SelectedMidiChannel { get; }
    int SelectedSampleRate { get; }
    int SelectedBufferSize { get; }
    float Volume { get; set; }
    Task InitializeAsync();
    void SelectAudioOutputDevice(string? deviceId);
    void SelectNotePlaybackMode(NotePlaybackMode notePlaybackMode);
    void SelectBufferSize(int bufferSize);
    void SelectMidiChannel(int midiChannel);
    void SelectMidiInputDevice(string? deviceId);
    void SelectSampleRate(int sampleRate);
    void NoteOn(float frequency);
    void NoteOff();
    string RunAudioTest();
    string RunMidiTest();
}
