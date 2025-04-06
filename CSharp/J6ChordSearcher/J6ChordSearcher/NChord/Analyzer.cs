namespace J6ChordSearcher.NChord;

public static class Analyzer
{
  /// <summary>
  /// Finds possible chords consisting of the given notes.
  /// </summary>
  /// <param name="notes">List of note names arranged from lower to higher (e.g., ["C", "Eb", "G"]).</param>
  /// <returns>List of possible Chord objects.</returns>
  /// <exception cref="ArgumentException">Thrown if the notes list is null or empty.</exception>
  public static List<Chord> FindChordsFromNotes(List<string> notes)
  {
    if (notes == null || !notes.Any())
      throw new ArgumentException("Please specify notes which consist a chord.");

    var root = notes[0];
    var rootAndPositions = new List<(string Root, int[] Positions)>();
    foreach (var rotatedNotes in GetAllRotatedNotes(notes))
    {
      var rotatedRoot = rotatedNotes[0];
      rootAndPositions.Add((rotatedRoot, NotesToPositions(rotatedNotes, rotatedRoot)));
    }

    var chords = new List<Chord>();
    foreach (var (tempRoot, positions) in rootAndPositions)
    {
      var quality = QualityManager.Instance.FindQualityFromComponents(positions);
      if (quality == null)
        continue;

      var chordName = tempRoot == root
        ? $"{root}{quality.QualityString}"
        : $"{tempRoot}{quality.QualityString}/{root}";
      chords.Add(new Chord(chordName));
    }

    return chords;
  }

  /// <summary>
  /// Gets the semitone positions of notes relative to a root note.
  /// </summary>
  /// <param name="notes">List of note names.</param>
  /// <param name="root">Root note for position calculation.</param>
  /// <returns>List of semitone positions relative to the root.</returns>
  /// <example>
  /// <code>
  /// var positions = Analyzer.NotesToPositions(new List&lt;string&gt; { "C", "E", "G" }, "C");
  /// Console.WriteLine(string.Join(", ", positions)); // Outputs: "0, 4, 7"
  /// </code>
  /// </example>
  public static int[] NotesToPositions(List<string> notes, string root)
  {
    if (notes == null || !notes.Any() || string.IsNullOrEmpty(root))
      throw new ArgumentException("Notes list and root cannot be null or empty.");

    var rootPos = Utils.NoteToVal(root);
    var currentPos = rootPos;
    var positions = new List<int>();

    foreach (var note in notes)
    {
      var notePos = Utils.NoteToVal(note);
      if (notePos < currentPos)
      {
        notePos += 12 * ((currentPos - notePos) / 12 + 1);
      }
      positions.Add(notePos - rootPos);
      currentPos = notePos;
    }

    return positions.ToArray();
  }

  /// <summary>
  /// Gets all possible rotations of a list of notes.
  /// </summary>
  /// <param name="notes">List of note names.</param>
  /// <returns>List of rotated note lists (e.g., ["A", "C", "E"] -> [["A", "C", "E"], ["C", "E", "A"], ["E", "A", "C"]]).</returns>
  public static List<List<string>> GetAllRotatedNotes(List<string> notes)
  {
    if (notes == null || !notes.Any())
      throw new ArgumentException("Notes list cannot be null or empty.");

    var notesList = new List<List<string>>();
    for (var x = 0; x < notes.Count; x++)
    {
      var rotated = notes.Skip(x).Concat(notes.Take(x)).ToList();
      notesList.Add(rotated);
    }
    return notesList;
  }
}