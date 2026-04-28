using MutaSynthEngine;
using MutaSynthEngine.Platforms.Windows;

namespace MutaSynthSpecification;

public sealed class LiveOscillatorSampleProviderSpecification
{
    [Test]
    public void ShouldEaseReleasedVoiceInsteadOfDroppingToSilenceImmediately()
    {
        //bug TODO: Verify compensated and uncompensated filter types diverge audibly at higher resonance.
        //bug TODO: Verify post-drive saturation changes output without changing filter tracking.
        //bug TODO: Verify higher filter drive increases harmonic density without changing the open-filter level too much.

        // GIVEN
        var sut = new LiveOscillatorSampleProvider(48_000, 1, 1.0f);
        sut.SetOscillatorParameters(OscillatorWaveform.Sine, 0, 0, 0, 100, false);
        sut.SetFilterParameters(FilterType.LpLdr12, 128.0f, 0.0f, 100, 0.0f, FilterDriveRoute.Pre);
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
        sut.SetFilterParameters(FilterType.LpLdr12, 128.0f, 0.0f, 100, 0.0f, FilterDriveRoute.Pre);
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

    [Test]
    public void ShouldLeaveSawWaveUntouchedWhenFilterIsFullyOpen()
    {
        // GIVEN
        const int sampleRate = 48_000;
        const float frequency = 440.0f;
        const int warmupSampleCount = 2_048;
        var sut = new LiveOscillatorSampleProvider(sampleRate, 1, 1.0f);
        sut.SetOscillatorParameters(OscillatorWaveform.Saw, 0, 0, 0, 100, false);
        sut.SetFilterParameters(FilterType.LpLdr12, 128.0f, 0.0f, 100, 0.0f, FilterDriveRoute.Pre);
        sut.NoteOn(frequency);
        var warmupBuffer = new float[warmupSampleCount];
        sut.Read(warmupBuffer, 0, warmupBuffer.Length);

        // WHEN
        var actualBuffer = new float[64];
        sut.Read(actualBuffer, 0, actualBuffer.Length);
        var expectedBuffer = CreateExpectedSawBuffer(sampleRate, frequency, warmupSampleCount, actualBuffer.Length);

        // THEN
        actualBuffer.Zip(expectedBuffer, (actual, expected) => MathF.Abs(actual - expected)).Max().Should().BeLessThan(0.000001f);
    }

    [Test]
    public void ShouldReduceBrightnessWhenLoweringFilterCutoff()
    {
        // GIVEN
        var openFilter = new LiveOscillatorSampleProvider(48_000, 1, 1.0f);
        openFilter.SetOscillatorParameters(OscillatorWaveform.Saw, 0, 0, 0, 100, false);
        openFilter.SetFilterParameters(FilterType.LpLdr12, 128.0f, 0.0f, 100, 0.0f, FilterDriveRoute.Pre);
        openFilter.NoteOn(880.0f);

        var closedFilter = new LiveOscillatorSampleProvider(48_000, 1, 1.0f);
        closedFilter.SetOscillatorParameters(OscillatorWaveform.Saw, 0, 0, 0, 100, false);
        closedFilter.SetFilterParameters(FilterType.LpLdr12, 16.0f, 0.0f, 100, 0.0f, FilterDriveRoute.Pre);
        closedFilter.NoteOn(880.0f);

        var warmupBuffer = new float[1024];
        openFilter.Read(warmupBuffer, 0, warmupBuffer.Length);
        closedFilter.Read(warmupBuffer, 0, warmupBuffer.Length);

        // WHEN
        var openBuffer = new float[1024];
        openFilter.Read(openBuffer, 0, openBuffer.Length);

        var closedBuffer = new float[1024];
        closedFilter.Read(closedBuffer, 0, closedBuffer.Length);

        var openBrightness = AverageAbsoluteDelta(openBuffer);
        var closedBrightness = AverageAbsoluteDelta(closedBuffer);

        // THEN
        closedBrightness.Should().BeLessThan(openBrightness * 0.7f);
    }

    [Test]
    public void ShouldRingStronglyAfterReleaseAtMaximumResonance()
    {
        // GIVEN
        var sut = new LiveOscillatorSampleProvider(48_000, 1, 1.0f);
        sut.SetOscillatorParameters(OscillatorWaveform.Saw, 0, 0, 0, 100, false);
        sut.SetFilterParameters(FilterType.LpLdr14, 24.0f, 128.0f, 100, 0.0f, FilterDriveRoute.Pre);
        sut.NoteOn(220.0f);
        var warmupBuffer = new float[2_048];
        sut.Read(warmupBuffer, 0, warmupBuffer.Length);

        // WHEN
        sut.NoteOff(220.0f);
        var releaseBuffer = new float[4_096];
        sut.Read(releaseBuffer, 0, releaseBuffer.Length);
        var lateTailPeak = releaseBuffer.Skip(2_000).Max(sample => MathF.Abs(sample));

        // THEN
        lateTailPeak.Should().BeGreaterThan(0.02f);
    }

    [Test]
    public void ShouldBoostLowFrequencyBodyMoreForFatFilterThanLadderFilter()
    {
        // GIVEN
        var ladderFilter = new LiveOscillatorSampleProvider(48_000, 1, 1.0f);
        ladderFilter.SetOscillatorParameters(OscillatorWaveform.Saw, 0, 0, 0, 100, false);
        ladderFilter.SetFilterParameters(FilterType.LpLdr12, 36.0f, 48.0f, 100, 24.0f, FilterDriveRoute.Pre);
        ladderFilter.NoteOn(220.0f);

        var fatFilter = new LiveOscillatorSampleProvider(48_000, 1, 1.0f);
        fatFilter.SetOscillatorParameters(OscillatorWaveform.Saw, 0, 0, 0, 100, false);
        fatFilter.SetFilterParameters(FilterType.LpFat12, 36.0f, 48.0f, 100, 24.0f, FilterDriveRoute.Pre);
        fatFilter.NoteOn(220.0f);

        var warmupBuffer = new float[1024];
        ladderFilter.Read(warmupBuffer, 0, warmupBuffer.Length);
        fatFilter.Read(warmupBuffer, 0, warmupBuffer.Length);

        // WHEN
        var ladderBuffer = new float[1024];
        ladderFilter.Read(ladderBuffer, 0, ladderBuffer.Length);

        var fatBuffer = new float[1024];
        fatFilter.Read(fatBuffer, 0, fatBuffer.Length);

        var ladderBody = AverageAbsoluteAmplitude(ladderBuffer);
        var fatBody = AverageAbsoluteAmplitude(fatBuffer);

        // THEN
        fatBody.Should().BeGreaterThan(ladderBody * 1.1f);
    }

    private static float AverageAbsoluteDelta(float[] buffer)
    {
        return buffer
            .Zip(buffer.Skip(1), (current, next) => MathF.Abs(next - current))
            .Average();
    }

    private static float AverageAbsoluteAmplitude(float[] buffer)
    {
        return buffer.Average(sample => MathF.Abs(sample));
    }

    private static float[] CreateExpectedSawBuffer(int sampleRate, float frequency, int elapsedSamples, int sampleCount)
    {
        const float headroomGain = 0.5f;
        var phaseIncrement = frequency / sampleRate;
        var phase = (elapsedSamples * phaseIncrement) % 1.0f;
        var buffer = new float[sampleCount];

        for (var i = 0; i < sampleCount; i++)
        {
            buffer[i] = (((2.0f * phase) - 1.0f) * headroomGain);
            phase += phaseIncrement;
            if (phase >= 1.0f)
            {
                phase -= 1.0f;
            }
        }

        return buffer;
    }
}
