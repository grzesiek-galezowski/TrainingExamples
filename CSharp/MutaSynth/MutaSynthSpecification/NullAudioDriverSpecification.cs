using MutaSynthEngine;

namespace MutaSynthSpecification;

public sealed class NullAudioDriverSpecification
{
    [Test]
    public async Task ShouldDefaultVolumeToHalf()
    {
        //bug TODO: Verify InitializeAsync remains safe when called multiple times.
        //bug TODO: Verify NoteOn and NoteOff remain no-op for unsupported platforms.
        //bug TODO: Verify selecting an unknown MIDI device leaves the current selection unchanged.
        //bug TODO: Verify selecting an unknown audio output device leaves the current selection unchanged.

        // GIVEN
        var sut = new NullAudioDriver();

        // WHEN
        await sut.InitializeAsync();

        // THEN
        sut.Volume.Should().Be(0.5f);
    }

    [Test]
    public void ShouldExposeMidiChannelSelection()
    {
        // GIVEN
        var sut = new NullAudioDriver();

        // WHEN
        sut.SelectedMidiChannel.Should().Be(1);
        sut.SelectMidiChannel(7);

        // THEN
        sut.SelectedMidiChannel.Should().Be(7);
    }

    [Test]
    public void ShouldExposeEmptyDeviceSelections()
    {
        // GIVEN
        var sut = new NullAudioDriver();

        // WHEN
        sut.SelectMidiInputDevice("midi-1");
        sut.SelectAudioOutputDevice("audio-1");

        // THEN
        sut.MidiInputDevices.Should().BeEmpty();
        sut.AudioOutputDevices.Should().BeEmpty();
        sut.SelectedMidiInputDeviceId.Should().Be("midi-1");
        sut.SelectedAudioOutputDeviceId.Should().Be("audio-1");
    }

    [Test]
    public void ShouldAllowRunningDiagnosticTests()
    {
        // GIVEN
        var sut = new NullAudioDriver();

        // WHEN
        var testMidiResult = sut.RunMidiTest();
        var testAudioResult = sut.RunAudioTest();

        // THEN
        testMidiResult.Should().NotBeNullOrWhiteSpace();
        testAudioResult.Should().NotBeNullOrWhiteSpace();
    }

    [Test]
    public void ShouldExposeAudioSettingsSelection()
    {
        // GIVEN
        var sut = new NullAudioDriver();

        // WHEN
        sut.SelectedSampleRate.Should().Be(48_000);
        sut.SelectedBufferSize.Should().Be(512);
        sut.SelectSampleRate(44_100);
        sut.SelectBufferSize(128);

        // THEN
        sut.SelectedSampleRate.Should().Be(44_100);
        sut.SelectedBufferSize.Should().Be(128);
    }

    [Test]
    public void ShouldExposeNotePlaybackModeSelection()
    {
        // GIVEN
        var sut = new NullAudioDriver();

        // WHEN
        sut.SelectedNotePlaybackMode.Should().Be(NotePlaybackMode.Monophonic);
        sut.SelectNotePlaybackMode(NotePlaybackMode.Polyphonic);

        // THEN
        sut.NotePlaybackModes.Should().Equal([NotePlaybackMode.Monophonic, NotePlaybackMode.Polyphonic]);
        sut.SelectedNotePlaybackMode.Should().Be(NotePlaybackMode.Polyphonic);
    }

    [Test]
    public void ShouldExposeOscillatorDefaults()
    {
        // GIVEN
        var sut = new NullAudioDriver();

        // WHEN
        var availableWaveforms = sut.Waveforms;
        var availableBitReduxLevels = sut.BitReduxLevels;

        // THEN
        availableWaveforms.Should().Equal([OscillatorWaveform.Sine, OscillatorWaveform.Triangle, OscillatorWaveform.TriSaw, OscillatorWaveform.Saw, OscillatorWaveform.Square]);
        availableBitReduxLevels.Should().Equal([0, 2, 3, 4, 5, 6, 7, 8, 9, 10, 12, 16]);
        sut.SelectedWaveform.Should().Be(OscillatorWaveform.Saw);
        sut.SelectedSemiOffset.Should().Be(0);
        sut.SelectedCentsOffset.Should().Be(0);
        sut.SelectedBitRedux.Should().Be(0);
        sut.SelectedKeytrackPercent.Should().Be(100);
        sut.IsOscillatorLoggingEnabled.Should().BeFalse();
    }

    [Test]
    public void ShouldExposeOscillatorParameterSelection()
    {
        // GIVEN
        var sut = new NullAudioDriver();

        // WHEN
        sut.SelectSemiOffset(36);
        sut.SelectWaveform(OscillatorWaveform.Square);
        sut.SelectCentsOffset(-50);
        sut.SelectBitRedux(8);
        sut.SelectKeytrackPercent(200);
        sut.SelectOscillatorLogging(true);

        // THEN
        sut.SelectedSemiOffset.Should().Be(36);
        sut.SelectedWaveform.Should().Be(OscillatorWaveform.Square);
        sut.SelectedCentsOffset.Should().Be(-50);
        sut.SelectedBitRedux.Should().Be(8);
        sut.SelectedKeytrackPercent.Should().Be(200);
        sut.IsOscillatorLoggingEnabled.Should().BeTrue();
    }
}