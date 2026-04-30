using NAudio.Wave;
using System;
using System.Collections.Generic;

namespace MutaSynthEngine.Platforms.Windows;

public sealed class PolyphonicOscillator
{
    private const float BaseKeytrackFrequency = 440.0f;
    private const double TwoPi = Math.PI * 2.0;

    private readonly int _sampleRate;
    private readonly Dictionary<float, VoicePhase> _voices = [];

    public OscillatorWaveform Waveform { get; set; } = OscillatorWaveform.Saw;
    public int SemiOffset { get; set; } = 0;
    public int CentsOffset { get; set; } = 0;
    public int KeytrackPercent { get; set; } = 100;
    public int BitRedux { get; set; } = 0;

    public PolyphonicOscillator(int sampleRate)
    {
        _sampleRate = sampleRate;
    }

    public void NoteOn(float frequency)
    {
        _voices[frequency] = new VoicePhase();
    }

    public void NoteOff(float frequency)
    {
        // Don't remove here. The main synth handles voice lifecycle and will call RemoveVoice when envelope finishes.
    }

    public void RemoveVoice(float frequency)
    {
        _voices.Remove(frequency);
    }

    public float Process(float frequency)
    {
        if (!_voices.TryGetValue(frequency, out var voice))
        {
            return 0.0f;
        }

        var sample = CreateWaveSample(voice.Phase, Waveform);
        sample = ApplyBitReduction(sample);

        voice.Phase += CalculateEffectiveFrequency(frequency, SemiOffset, CentsOffset) / _sampleRate;
        if (voice.Phase >= 1.0)
        {
            voice.Phase -= Math.Floor(voice.Phase);
        }

        return sample;
    }

    private float ApplyBitReduction(float sample)
    {
        if (BitRedux == 0)
        {
            return sample;
        }

        var steps = 1 << (BitRedux - 1);
        return MathF.Round(sample * steps) / steps;
    }

    private float CreateWaveSample(double phase, OscillatorWaveform waveform)
    {
        return waveform switch
        {
            OscillatorWaveform.Sine => (float)Math.Sin(phase * TwoPi),
            OscillatorWaveform.Triangle => 1.0f - (4.0f * MathF.Abs((float)phase - 0.5f)),
            OscillatorWaveform.TriSaw => CreateTriSawSample((float)phase),
            OscillatorWaveform.Square => phase < 0.5 ? 1.0f : -1.0f,
            _ => (float)((2.0 * phase) - 1.0)
        };
    }

    private static float CreateTriSawSample(float phase)
    {
        var triangle = 1.0f - (4.0f * MathF.Abs(phase - 0.5f));
        var saw = (2.0f * phase) - 1.0f;
        return (triangle + saw) * 0.5f;
    }

    private float CalculateEffectiveFrequency(float baseFrequency, int semiOffset, int centsOffset)
    {
        var noteSemitoneOffset = 12.0 * Math.Log2(baseFrequency / BaseKeytrackFrequency);
        var trackedFrequency = BaseKeytrackFrequency * Math.Pow(2.0, (noteSemitoneOffset * KeytrackPercent) / 1200.0);
        var tuningOffset = semiOffset + (centsOffset / 100.0);
        return (float)(trackedFrequency * Math.Pow(2.0, tuningOffset / 12.0));
    }

    private class VoicePhase 
    {
        public double Phase { get; set; }
    }
}
