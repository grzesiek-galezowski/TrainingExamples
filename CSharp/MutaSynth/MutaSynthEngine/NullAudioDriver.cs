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
    public ImmutableArray<int> MidiChannels { get; } = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];
    public ImmutableArray<int> SampleRates { get; } = [22_050, 44_100, 48_000, 96_000];
    public ImmutableArray<int> BufferSizes { get; } = [64, 128, 256, 512, 1024];
    public string? SelectedMidiInputDeviceId { get; private set; }
    public string? SelectedAudioOutputDeviceId { get; private set; }
    public NotePlaybackMode SelectedNotePlaybackMode { get; private set; } = NotePlaybackMode.Monophonic;
    public int SelectedMidiChannel { get; private set; } = 1;
    public int SelectedSampleRate { get; private set; } = 48_000;
    public int SelectedBufferSize { get; private set; } = 512;
    public float Volume { get; set; } = 0.5f;
    public Task InitializeAsync() => Task.CompletedTask;
    public void SelectAudioOutputDevice(string? deviceId) => SelectedAudioOutputDeviceId = deviceId;
    public void SelectNotePlaybackMode(NotePlaybackMode notePlaybackMode) => SelectedNotePlaybackMode = notePlaybackMode;
    public void SelectBufferSize(int bufferSize) => SelectedBufferSize = bufferSize;
    public void SelectMidiChannel(int midiChannel) => SelectedMidiChannel = midiChannel;
    public void SelectMidiInputDevice(string? deviceId) => SelectedMidiInputDeviceId = deviceId;
    public void SelectSampleRate(int sampleRate) => SelectedSampleRate = sampleRate;
    public void NoteOn(float frequency) { }
    public void NoteOff() { }
    public string RunAudioTest() => "Audio test is not available on this platform.";
    public string RunMidiTest() => "MIDI test is not available on this platform.";
    public void Dispose() { }
}
