using System;
using System.Collections.Generic;
using System.Linq;

namespace Pychord
{
  public partial class Chord
  {
    // Private fields mirroring Python attributes
    private string _chord;          // Full chord name (e.g., "C", "Am7", "F#m7-5/A")
    private string _root;           // Root note (e.g., "C", "A", "F#")
    private Quality _quality;       // Chord quality (e.g., maj, m7, m7-5)
    private List<string> _appended; // Appended notes
    private string _on;             // Base note of slash chord

    /// <summary>
    /// Constructor for Chord instance.
    /// </summary>
    /// <param name="chord">Name of chord (e.g., "C", "Am7", "F#m7-5/A").</param>
    public Chord(string chord)
    {
      if (string.IsNullOrEmpty(chord))
        throw new ArgumentException("Chord name cannot be empty");

      // Parse the chord string using the external parse method
      var (root, quality, appended, on) = Parse(chord);
      _chord = chord;
      _root = root;
      _quality = quality;
      _appended = appended;
      _on = on;

      _append_on_chord();
    }

    public void Deconstruct(out string root, out Quality quality, out List<string> appended, out string on)
    {
      root = _root;
      quality = _quality;
      appended = _appended; //bug mutable collection
      on = _on;
    }

    // Properties (equivalent to Python @property)
    public string ChordName => _chord;
    public string Root => _root;
    public Quality Quality => _quality;
    public List<string> Appended => _appended;
    public string On => _on;

    // Override ToString (equivalent to Python __str__ and __unicode__)
    public override string ToString()
    {
      return _chord;
    }

    // Override for representation (equivalent to Python __repr__)
    public string ToRepresentation()
    {
      return $"<Chord: {_chord}>";
    }

    // Equality comparison (equivalent to Python __eq__)
    public override bool Equals(object obj)
    {
      if (!(obj is Chord other))
        throw new ArgumentException($"Cannot compare Chord with {obj?.GetType().Name ?? "null"}");

      if (Utils.NoteToVal(_root) != Utils.NoteToVal(other._root))
        return false;
      if (!_quality.Equals(other._quality))
        return false;
      if (!_appended.SequenceEqual(other._appended))
        return false;
      if (!string.IsNullOrEmpty(_on) && !string.IsNullOrEmpty(other._on))
      {
        if (Utils.NoteToVal(_on) != Utils.NoteToVal(other._on))
          return false;
      }
      return true;
    }

    // Override GetHashCode for consistency with Equals
    public override int GetHashCode()
    {
      return HashCode.Combine(_root, _quality, _appended, _on);
    }

    // Inequality (equivalent to Python __ne__)
    public static bool operator !=(Chord left, Chord right) => !(left == right);
    public static bool operator ==(Chord left, Chord right) => left?.Equals(right) ?? right is null;

    /// <summary>
    /// Creates a Chord from a note index in a scale.
    /// </summary>
    /// <param name="note">Scale degree of the chord's root (1-7).</param>
    /// <param name="quality">Quality of the chord (e.g., "m7", "sus4").</param>
    /// <param name="scale">Base scale (e.g., "Cmaj", "Amin").</param>
    /// <param name="diatonic">If true, quality is determined by the scale.</param>
    /// <param name="chromatic">Chromatic alteration in semitones.</param>
    /// <returns>A new Chord instance.</returns>
    public static Chord FromNoteIndex(int note, string quality, string scale, bool diatonic = false, int chromatic = 0)
    {
      if (note < 1 || note > 8)
        throw new ArgumentException($"Invalid note {note}, must be between 1 and 8");

      // Extract scale root and type (e.g., "Cmaj" -> "C" and "maj")
      string scaleRoot = scale[..^3];
      string scaleType = scale.Substring(scale.Length - 3);
      int[] scaleDegrees = Constants.Scales.RelativeKeyDict[scaleType];
      int relativeKey = scaleDegrees[note - 1];
      int rootNum = Utils.NoteToVal(scaleRoot) + chromatic;
      string root = Utils.ValToNote((rootNum + relativeKey) % 12);

      string finalQuality = quality;
      if (diatonic)
      {
        // Calculate diatonic chord components
        int third = scaleDegrees[(note + 1) % 7];
        int fifth = scaleDegrees[(note + 3) % 7];
        int seventh = scaleDegrees[(note + 5) % 7];

        int[] components;
        if (string.IsNullOrEmpty(quality) || quality == "-" || quality == "maj" || quality == "m" || quality == "min")
        {
          components = GetDiatonicChord([relativeKey, third, fifth]);
        }
        else if (quality == "7" || quality == "M7" || quality == "maj7" || quality == "m7")
        {
          components = GetDiatonicChord([relativeKey, third, fifth, seventh]);
        }
        else
        {
          throw new NotImplementedException("Only generic chords (triads, sevenths) are supported");
        }

        var qualityManager = new QualityManager();
        finalQuality = qualityManager.FindQualityFromComponents(components)?.QualityString
            ?? throw new Exception($"Quality with components [{string.Join(", ", components)}] not found");
      }

      return new Chord($"{root}{finalQuality}");
    }

    /// <summary>
    /// Returns information about the chord for display.
    /// </summary>
    public string Info()
    {
      return $"{_chord}\nroot={_root}\nquality={_quality}\nappended={string.Join(", ", _appended)}\non={_on ?? "None"}";
    }

    /// <summary>
    /// Transposes the chord by a number of semitones.
    /// </summary>
    /// <param name="trans">Number of semitones to transpose.</param>
    /// <param name="scale">Reference scale (default "C").</param>
    public void Transpose(int trans, string scale = "C")
    {
      if (!int.TryParse(trans.ToString(), out _))
        throw new ArgumentException($"Expected integer, not {trans.GetType().Name}");

      _root = Utils.TransposeNote(_root, trans, scale);
      if (!string.IsNullOrEmpty(_on))
        _on = Utils.TransposeNote(_on, trans, scale);
      _reconfigure_chord();
    }

    /// <summary>
    /// Returns the component notes of the chord.
    /// </summary>
    /// <param name="visible">If true, returns note names; otherwise, returns semitone values.</param>
    public List<object> Components(bool visible = true)
    {
      return _quality.GetComponentsVisible(_root, visible);
    }

    /// <summary>
    /// Returns the component notes with pitch (e.g., ["C4", "E4", "G4"]).
    /// </summary>
    /// <param name="rootPitch">The pitch of the root note (e.g., 4 for C4).</param>
    public List<string> ComponentsWithPitch(int rootPitch)
    {
      var components = _quality.GetComponents(_root);
      if (components[0] < 0)
        components = components.Select(c => c + 12).ToList();
      return components.Select(c => $"{Utils.ValToNote(c, _root)}{rootPitch + c / 12}").ToList();
    }

    // Private helper methods
    private void _append_on_chord()
    {
      if (!string.IsNullOrEmpty(_on))
        _quality.AppendOnChord(_on, _root);
    }

    private void _reconfigure_chord()
    {
      _chord = $"{_root}{_quality.QualityString}{Utils.DisplayAppended(_appended)}{Utils.DisplayOn(_on)}";
    }

    private static int[] GetDiatonicChord(int[] chord)
    {
      var uninverted = new List<int>();
      foreach (int note in chord)
      {
        if (!uninverted.Any())
          uninverted.Add(note);
        else if (note > uninverted.Last())
          uninverted.Add(note);
        else
          uninverted.Add(note + 12);
      }
      return uninverted.Select(x => x - uninverted[0]).ToArray();
    }
  }
}

