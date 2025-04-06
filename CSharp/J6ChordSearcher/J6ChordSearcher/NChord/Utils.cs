namespace J6ChordSearcher.NChord;

public static class Utils
{
  public static readonly string[] NoteNames = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"];

  /// <summary>
  /// Gets the chromatic index value of a note (0–11).
  /// </summary>
  /// <param name="note">The note name (e.g., "C", "B", "C#", "Db").</param>
  /// <returns>The index value (0 for C, 11 for B).</returns>
  /// <exception cref="ArgumentException">Thrown if the note is unknown.</exception>
  /// <example>
  /// <code>
  /// Console.WriteLine(Utils.NoteToVal("C")); // Outputs: 0
  /// Console.WriteLine(Utils.NoteToVal("B")); // Outputs: 11
  /// </code>
  /// </example>
  public static int NoteToVal(string note)
  {
    if (string.IsNullOrEmpty(note) || !Scales.NoteValDict.TryGetValue(note, out var val))
      throw new ArgumentException($"Unknown note: {note}");
    return val;
  }

  /// <summary>
  /// Returns a note name by its chromatic index in a given scale.
  /// </summary>
  /// <param name="val">The chromatic index (0–11, or any integer; will be normalized).</param>
  /// <param name="scale">The root note of the scale (default "C").</param>
  /// <returns>The note name relative to the scale.</returns>
  /// <example>
  /// <code>
  /// Console.WriteLine(Utils.ValToNote(0));      // Outputs: "C"
  /// Console.WriteLine(Utils.ValToNote(11, "D")); // Outputs: "C#"
  /// </code>
  /// </example>
  public static string ValToNote(int val, string scale = "C")
  {
    if (!Scales.ScaleValDict.ContainsKey(scale))
      throw new ArgumentException($"Unknown scale: {scale}");
    val = (val % 12 + 12) % 12; // Normalize to 0–11, handling negative values
    return Scales.ScaleValDict[scale][val];
  }

  /// <summary>
  /// Converts a negative MIDI note transposition value (in semitones) to its equivalent positive value.
  /// The result is normalized to the range [0, 11], representing one octave.
  /// </summary>
  /// <param name="transposition">The transposition value in semitones (can be negative).</param>
  /// <returns>The equivalent positive transposition value (0 to 11).</returns>
  public static int ToPositiveTransposition(int transposition)
  {
    // Normalize to the range [0, 11] by adding 12 until positive, then taking modulo 12
    return (transposition % 12 + 12) % 12;
  }

  // Additional utility method for rotating lists, mimicking Python's list slicing
  public static T[] Rotate<T>(T[] array, int steps)
  {
    var result = new T[array.Length];
    for (var i = 0; i < array.Length; i++)
    {
      var newIndex = (i + steps) % array.Length;
      if (newIndex < 0) newIndex += array.Length;
      result[newIndex] = array[i];
    }
    return result;
  }

  /// <summary>
  /// Transposes a note by a specified number of semitones.
  /// </summary>
  /// <param name="note">The note to transpose (e.g., "C", "D#").</param>
  /// <param name="transpose">The number of semitones to transpose (positive or negative).</param>
  /// <param name="scale">The reference scale for note naming (default "C").</param>
  /// <returns>The transposed note as a string.</returns>
  /// <example>
  /// <code>
  /// Console.WriteLine(Utils.TransposeNote("C", 1));  // Outputs: "C#"
  /// Console.WriteLine(Utils.TransposeNote("D", 4, "A"));  // Outputs: "F#"
  /// </code>
  /// </example>
  public static string TransposeNote(string note, int transpose, string scale = "C")
  {
    if (string.IsNullOrEmpty(note))
      throw new ArgumentException("Note cannot be null or empty");

    var val = NoteToVal(note);
    val += transpose;
    return ValToNote(val, scale);
  }

  public static string DisplayAppended(List<string> appended)
  {
    return string.Empty; //bug
  }

  /// <summary>
  /// Formats a bass note for slash chord notation.
  /// </summary>
  /// <param name="onNote">The bass note (e.g., "G" for "C/G").</param>
  /// <returns>A string with "/note" if onNote is non-empty, otherwise an empty string.</returns>
  public static string DisplayOn(string onNote)
  {
    return string.IsNullOrEmpty(onNote) ? "" : $"/{onNote}";
  }
}