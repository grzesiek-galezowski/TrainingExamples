using LanguageExt;

namespace J6ChordSearcher.NChord;

public partial class Chord
{
  // Private fields mirroring Python attributes
  private string _chord;          // Full chord name (e.g., "C", "Am7", "F#m7-5/A")
  private string _root;           // Root note (e.g., "C", "A", "F#")
  private Seq<Quality> _qualities;       // Chord quality (e.g., maj, m7, m7-5)
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
    var (root, qualities, appended, on) = Parse(chord);
    _chord = chord;
    _root = root;
    _qualities = qualities;
    _appended = appended;
    _on = on;

    _append_on_chord();
  }

  public void Deconstruct(out string root, out Seq<Quality> qualities, out List<string> appended, out string on)
  {
    root = _root;
    qualities = _qualities;
    appended = _appended; //bug mutable collection
    on = _on;
  }

  // Properties (equivalent to Python @property)
  public string ChordName => _chord;
  public string Root => _root;
  public Seq<Quality> Qualities => _qualities;
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
    if (!_qualities.Equals(other._qualities))
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
    return HashCode.Combine(_root, _qualities, _appended, _on);
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
    var scaleRoot = scale[..^3];
    var scaleType = scale.Substring(scale.Length - 3);
    var scaleDegrees = Scales.RelativeKeyDict[scaleType];
    var relativeKey = scaleDegrees[note - 1];
    var rootNum = Utils.NoteToVal(scaleRoot) + chromatic;
    var root = Utils.ValToNote((rootNum + relativeKey) % 12);

    var finalQuality = quality;
    if (diatonic)
    {
      // Calculate diatonic chord components
      var third = scaleDegrees[(note + 1) % 7];
      var fifth = scaleDegrees[(note + 3) % 7];
      var seventh = scaleDegrees[(note + 5) % 7];

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
    return $"{_chord}\nroot={_root}\nquality={_qualities}\nappended={string.Join(", ", _appended)}\non={_on ?? "None"}";
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

  //bug /// <summary>
  //bug /// Returns the component notes of the chord.
  //bug /// </summary>
  //bug /// <param name="visible">If true, returns note names; otherwise, returns semitone values.</param>
  //bug public List<object> Components(bool visible = true)
  //bug {
  //bug   return _qualities.GetComponentsVisible(_root, visible);
  //bug }
  //bug 
  //bug /// <summary>
  //bug /// Returns the component notes with pitch (e.g., ["C4", "E4", "G4"]).
  //bug /// </summary>
  //bug /// <param name="rootPitch">The pitch of the root note (e.g., 4 for C4).</param>
  //bug public List<string> ComponentsWithPitch(int rootPitch)
  //bug {
  //bug   var components = _qualities.GetComponents(_root);
  //bug   if (components[0] < 0)
  //bug     components = components.Select(c => c + 12).ToList();
  //bug   return components.Select(c => $"{Utils.ValToNote(c, _root)}{rootPitch + c / 12}").ToList();
  //bug }

  // Private helper methods
  private void _append_on_chord()
  {
    if (!string.IsNullOrEmpty(_on))
    {
      foreach (var quality in _qualities)
      {
        quality.AppendOnChord(_on, _root);
      }
    }
  }

  private void _reconfigure_chord()
  {
    _chord = $"{_root}{_qualities.First().QualityString}{Utils.DisplayAppended(_appended)}{Utils.DisplayOn(_on)}";
  }

  private static int[] GetDiatonicChord(int[] chord)
  {
    var uninverted = new List<int>();
    foreach (var note in chord)
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