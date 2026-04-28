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

    // Formant SVF parallel filter states
    private double _bp1ic1, _bp1ic2;
    private double _bp2ic1, _bp2ic2;
    private double _bp3ic1, _bp3ic2;

    private static readonly double[,] Formants = new double[,] {
        {730.0, 1090.0, 2440.0}, // A
        {530.0, 1840.0, 2480.0}, // E
        {270.0, 2290.0, 3010.0}, // I
        {400.0,  840.0, 2800.0}, // O
        {300.0,  870.0, 2240.0}  // U
    };

    private static readonly int[,] VowOrders = new int[,] {
        {0, 1, 2, 3, 4},
        {4, 3, 2, 1, 0},
        {0, 2, 1, 4, 3},
        {3, 4, 1, 2, 0},
        {1, 0, 3, 2, 4},
        {2, 1, 0, 4, 3},
        {4, 2, 3, 1, 0},
        {0, 3, 4, 1, 2}
    };

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
        _bp1ic1 = _bp1ic2 = 0.0;
        _bp2ic1 = _bp2ic2 = 0.0;
        _bp3ic1 = _bp3ic2 = 0.0;
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

        if (type == FilterType.HpSqu24)
        {
            // Squelchy TB-303-styled filter logic.
            // A 303 isn't really a 4-pole highpass, but adding its diode-clipped behavior 
            // over a heavily saturated 24dB Moog block with resonant phase shifting
            // gives that classic acidic squelchy 18-24dB "TB-like" lowpass peaking sound.
            return Math.Tanh(lp4 * 2.5) * 0.7; // Harder drive out mapping for acid squelch
        }

        // --- output tap ---
        // LpLdr12 / LpFat12  → LP2 = 12 dB/oct roll-off
        // LpLdr14 / LpFat14  → LP4 = 24 dB/oct roll-off
        return (type is FilterType.LpLdr12 or FilterType.LpFat12 ? lp2 : lp4) * 0.8;
    }

    public double ProcessFormant(double input, double cutoffUi, double resonance, int vowOrder, float formantControl)
    {
        // Simple 3-pole parallel vowel filtering using SVF bandpasses
        // Cutoff UI ranges [0, 128] determining position inside the chosen Vow Order sequence
        double pos = Math.Clamp(cutoffUi / 128.0, 0.0, 0.9999) * 4.0;
        int index = (int)pos;
        double frac = pos - index;

        int vow1 = VowOrders[vowOrder, index];
        int vow2 = VowOrders[vowOrder, index + 1];

        // Interpolate formants across the Vowel morph sequence
        double f1 = Formants[vow1, 0] * (1.0 - frac) + Formants[vow2, 0] * frac;
        double f2 = Formants[vow1, 1] * (1.0 - frac) + Formants[vow2, 1] * frac;
        double f3 = Formants[vow1, 2] * (1.0 - frac) + Formants[vow2, 2] * frac;

        // Control - how the lips are open horizontally (width of lips) -> spreads/shifts F2 and F3
        double widthFactor = 0.5 + (formantControl / 128.0); // scales 0.5x to 1.5x
        f2 *= widthFactor;
        f3 *= widthFactor;

        // Resonance - the size/scale of the lips. Bigger lips = lower base frequencies up to 0.8 scale. 
        double scale = 1.2 - (resonance / 128.0) * 0.4;
        f1 *= scale;
        f2 *= scale;
        f3 *= scale;

        // Resonance adds to internal Q of each parallel formant
        double q = 4.0 + (resonance / 128.0) * 16.0;
        double maxF = sampleRate * 0.49;

        f1 = Math.Clamp(f1, 20.0, maxF);
        f2 = Math.Clamp(f2, 20.0, maxF);
        f3 = Math.Clamp(f3, 20.0, maxF);

        double bp1 = RunSvf(input, f1, q, ref _bp1ic1, ref _bp1ic2);
        double bp2 = RunSvf(input, f2, q, ref _bp2ic1, ref _bp2ic2);
        double bp3 = RunSvf(input, f3, q, ref _bp3ic1, ref _bp3ic2);

        // Mix the bandpass filters in parallel to recreate vocal formants, adding slight soft saturation
        return Math.Tanh((bp1 + bp2 * 0.7 + bp3 * 0.4) * 2.0) * 0.7;
    }

    private double RunSvf(double input, double fc, double q, ref double ic1, ref double ic2)
    {
        double g = Math.Tan(Math.PI * fc / sampleRate);
        double k = 1.0 / q;
        double a1 = 1.0 / (1.0 + g * (g + k));
        double a2 = g * a1;
        double a3 = g * a2;

        double v3 = input - ic2;
        double v1 = a1 * ic1 + a2 * v3;        // Bandpass tap
        double v2 = ic2 + a2 * ic1 + a3 * v3;  // Lowpass tap

        ic1 = 2.0 * v1 - ic1;
        ic2 = 2.0 * v2 - ic2;

        return v1; 
    }
}
