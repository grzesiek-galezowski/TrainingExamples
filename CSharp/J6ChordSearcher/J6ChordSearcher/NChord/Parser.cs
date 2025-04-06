using System.Text.RegularExpressions;
using LanguageExt;

namespace J6ChordSearcher.NChord;

public partial class Chord
{
  // Regex to match inversion numbers (e.g., "/2" in "C/2")
  private static readonly Regex InversionRegex = new(@"/([0-9]+)", RegexOptions.Compiled);

  /// <summary>
  /// Parses a chord string into its components.
  /// </summary>
  /// <param name="chord">The chord string (e.g., "C", "Am7", "F#m7-5/G").</param>
  /// <returns>A tuple containing (root, quality, appended notes, bass note).</returns>
  /// <exception cref="ArgumentException">Thrown if the chord or notes are invalid.</exception>
  public static (string root, Seq<Quality> qualities, List<string> appended, string on) Parse(string chord)
  {
    try
    {
      if (string.IsNullOrEmpty(chord))
        throw new ArgumentException("Chord string cannot be null or empty");

      string root, rest;
      if (chord.Length > 1 && (chord[1] == 'b' || chord[1] == '#'))
      {
        root = chord.Substring(0, 2); // e.g., "C#", "Bb"
        rest = chord.Substring(2);
      }
      else
      {
        root = chord.Substring(0, 1); // e.g., "C", "A"
        rest = chord.Length > 1 ? chord.Substring(1) : "";
      }

      CheckNote(root);

      var inversion = 0;
      var inversionMatch = InversionRegex.Match(rest);
      if (inversionMatch.Success)
      {
        inversion = int.Parse(inversionMatch.Groups[1].Value);
        rest = InversionRegex.Replace(rest, "");
      }

      var on = "";
      var onChordIdx = rest.IndexOf('/');
      if (onChordIdx >= 0)
      {
        on = rest.Substring(onChordIdx + 1);
        rest = rest.Substring(0, onChordIdx);
        CheckNote(on);
      }

      var qualities = QualityManager.Instance.GetQualities(rest, inversion);
      // TODO: Implement parser for appended notes
      var appended = new List<string>();

      return (root, qualities, appended, on);
    }
    catch (Exception e)
    {
      throw new InvalidOperationException("Error when parsing chord " + chord, e);
    }
  }

  /// <summary>
  /// Validates a note against known note names.
  /// </summary>
  /// <param name="note">The note to check.</param>
  /// <exception cref="ArgumentException">Thrown if the note is invalid.</exception>
  private static void CheckNote(string note)
  {
    if (!Scales.NoteValDict.ContainsKey(note))
      throw new ArgumentException($"Invalid note: {note}");
  }
}