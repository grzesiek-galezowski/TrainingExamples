using MutaSynthEngine;
using MutaSynthEngine.Platforms.Windows;

namespace MutaSynthSpecification;

public sealed class LiveOscillatorSampleProviderSpecification
{
    [Test]
    public void ShouldEaseReleasedVoiceInsteadOfDroppingToSilenceImmediately()
    {
        // GIVEN
        var sut = new LiveOscillatorSampleProvider(48_000, 1, 1.0f);
        sut.SetOscillatorParameters(OscillatorWaveform.Sine, 0, 0, 0, 100, false);
        sut.NoteOn(440.0f);
        var attackBuffer = new float[128];
        sut.Read(attackBuffer, 0, attackBuffer.Length);

        // WHEN
        sut.NoteOff(440.0f);
        var releaseBuffer = new float[512];
        sut.Read(releaseBuffer, 0, releaseBuffer.Length);
        var tailBuffer = new float[1024];
        sut.Read(tailBuffer, 0, tailBuffer.Length);
        var sustainedReleaseSamples = releaseBuffer.Count(sample => MathF.Abs(sample) > 0.0001f);

        // THEN
        releaseBuffer[0].Should().NotBe(0.0f);
        sustainedReleaseSamples.Should().BeGreaterThan(256);
        tailBuffer[^1].Should().BeApproximately(0.0f, 0.01f);
    }

    [Test]
    public void ShouldAvoidAbruptLevelDropWhenQuicklyReleasingSecondPolyphonicVoice()
    {
        // GIVEN
        var sut = new LiveOscillatorSampleProvider(48_000, 1, 1.0f);
        sut.SetOscillatorParameters(OscillatorWaveform.Sine, 0, 0, 0, 100, false);
        sut.NoteOn(440.0f);
        var firstVoiceBuffer = new float[256];
        sut.Read(firstVoiceBuffer, 0, firstVoiceBuffer.Length);
        sut.NoteOn(660.0f);
        var stackedVoiceBuffer = new float[64];
        sut.Read(stackedVoiceBuffer, 0, stackedVoiceBuffer.Length);

        // WHEN
        sut.NoteOff(660.0f);
        var releaseBuffer = new float[768];
        sut.Read(releaseBuffer, 0, releaseBuffer.Length);
        var maximumDelta = releaseBuffer
            .Zip(releaseBuffer.Skip(1), (current, next) => MathF.Abs(next - current))
            .Max();

        // THEN
        maximumDelta.Should().BeLessThan(0.08f);
    }
}
