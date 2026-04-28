namespace MutaSynthEngine.Platforms.Windows;

/// <summary>
/// VA Moog Ladder filter — four cascaded one-pole lowpass stages with
/// non-linear resonance feedback, based on the Pirkle / Odin2 topology.
///
/// Each call to <see cref="Process"/> is a single sample transform.
/// The caller is responsible for resolving cutoff Hz (including any
/// keytracking) and for any drive/saturation applied around this block.
/// </summary>
internal sealed class MoogLadderFilter(int sampleRate)
{
    private const double TwoPi = Math.PI * 2.0;

    // Maximum resonance coefficient — 3.88 avoids hard-clipping during self-oscillation.
    private const double MaxK = 3.88;

    // Four one-pole stage z-1 stores (Pirkle formulation).
    private double _z1;
    private double _z2;
    private double _z3;
    private double _z4;

    private double _smoothedCutoffHz = -1.0;
    private double _smoothedResonance = -1.0;
    private const double SmoothFactor = 0.002;

    /// <summary>
    /// Clears all internal state. Call when playback stops so the next note
    /// starts from silence.
    /// </summary>
    public void Reset()
    {
        _z1 = 0.0;
        _z2 = 0.0;
        _z3 = 0.0;
        _z4 = 0.0;
        _smoothedCutoffHz = -1.0;
        _smoothedResonance = -1.0;
    }

    /// <summary>
    /// Processes one sample through the ladder filter.
    /// </summary>
    /// <param name="input">Input sample (any normalised range; kept consistent with caller).</param>
    /// <param name="cutoffHz">Resolved cutoff frequency in Hz, after keytracking.</param>
    /// <param name="resonance">Resonance in [0, 128]. 128 = strong self-oscillation.</param>
    /// <param name="type">Selects LP2 (12 dB/oct) or LP4 (24 dB/oct) output tap.</param>
    /// <returns>Filtered sample.</returns>
    public double Process(double input, double cutoffHz, double resonance, FilterType type)
    {
        if (_smoothedCutoffHz < 0.0)
        {
            _smoothedCutoffHz = cutoffHz;
            _smoothedResonance = resonance;
        }
        else
        {
            _smoothedCutoffHz += (cutoffHz - _smoothedCutoffHz) * SmoothFactor;
            _smoothedResonance += (resonance - _smoothedResonance) * SmoothFactor;
        }

        // Clamp cutoff up to 0.49 of sample rate to avoid Math.Tan blowing up, and map resonance to safe bonds
        var safeCutoff = Math.Clamp(_smoothedCutoffHz, 10.0, sampleRate * 0.49);
        var safeResonance = Math.Clamp(_smoothedResonance, 0.0, 128.0);

        // --- coefficient calculation (Pirkle / Odin2) ---

        // BZT prewarp: maps digital frequency to analogue prototype frequency.
        var wd = TwoPi * safeCutoff;
        var wa = 2.0 * sampleRate * Math.Tan(wd / (2.0 * sampleRate));
        var g = wa / (2.0 * sampleRate);

        // Feedforward gain shared by all four one-pole stages.
        var G = g / (1.0 + g);

        // Per-stage feedback weighting (derived from the signal-flow-graph).
        var oneOverOnePlusG = 1.0 / (1.0 + g);
        var beta1 = G * G * G * oneOverOnePlusG;
        var beta2 = G * G * oneOverOnePlusG;
        var beta3 = G * oneOverOnePlusG;
        var beta4 = oneOverOnePlusG;

        // Resonance coefficient k: maps [0, 128] → [0, 3.88].
        var k = (safeResonance / 128.0) * MaxK;

        // Sigma: the feedback signal that alpha0 corrects for.
        var sigma = beta1 * _z1 + beta2 * _z2 + beta3 * _z3 + beta4 * _z4;

        var gamma = G * G * G * G;
        var alpha0 = 1.0 / (1.0 + k * gamma);

        // --- non-linear input stage ---
        // Juiciness: Add a small drive to the input and apply proper saturation mapping
        var u = Math.Tanh(alpha0 * (input * 1.5 - k * sigma));

        // --- four cascaded VA one-pole LP stages (Pirkle formulation) ---
        // vn = (x - z) * G ;  y = vn + z ;  z_next = vn + y

        var vn1 = (u - _z1) * G;
        var lp1 = vn1 + _z1;
        _z1 = vn1 + lp1;

        // Mild saturation in alternate stages helps simulate analog warmth 
        // without collapsing the filter structure
        lp1 = Math.Tanh(lp1);

        var vn2 = (lp1 - _z2) * G;
        var lp2 = vn2 + _z2;
        _z2 = vn2 + lp2;

        var vn3 = (lp2 - _z3) * G;
        var lp3 = vn3 + _z3;
        _z3 = vn3 + lp3;

        lp3 = Math.Tanh(lp3);

        var vn4 = (lp3 - _z4) * G;
        var lp4 = vn4 + _z4;
        _z4 = vn4 + lp4;

        // --- output tap ---
        // LpLdr12 / LpFat12  → LP2 = 12 dB/oct roll-off
        // LpLdr14 / LpFat14  → LP4 = 24 dB/oct roll-off
        return (type is FilterType.LpLdr12 or FilterType.LpFat12 ? lp2 : lp4) * 0.8;
    }
}
