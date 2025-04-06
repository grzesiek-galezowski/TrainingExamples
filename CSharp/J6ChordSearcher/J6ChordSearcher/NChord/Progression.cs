namespace J6ChordSearcher.NChord;

public class Progression
{
  // Private field for the list of chords
  private readonly List<Chord> _chords;

  /// <summary>
  /// Constructor for Progression instance.
  /// </summary>
  /// <param name="initialChords">Initial chord or chords (string, Chord, or list thereof).</param>
  public Progression(object initialChords = null)
  {
    _chords = new List<Chord>();
    if (initialChords == null)
      return;

    switch (initialChords)
    {
      case Chord chord:
        _chords.Add(chord);
        break;
      case string chordString:
        _chords.Add(AsChord(chordString));
        break;
      case IEnumerable<object> chordList:
        _chords.AddRange(chordList.Select(chord => AsChord(chord)));
        break;
      default:
        throw new ArgumentException($"Cannot initialize Progression with argument of type {initialChords.GetType().Name}");
    }
  }

  // Property to access the chords (equivalent to Python @property)
  public List<Chord> Chords => _chords;

  // String representation (equivalent to Python __str__ and __unicode__)
  public override string ToString()
  {
    return string.Join(" | ", _chords.Select(chord => chord.ChordName));
  }

  // Representation for debugging (equivalent to Python __repr__)
  public string ToRepresentation()
  {
    return $"<Progression: {string.Join(" | ", _chords.Select(chord => chord.ChordName))}>";
  }

  // Addition operator (equivalent to Python __add__)
  public static Progression operator +(Progression left, Progression right)
  {
    var result = new Progression(left._chords);
    result._chords.AddRange(right._chords);
    return result;
  }

  // Length (equivalent to Python __len__)
  public int Count => _chords.Count;

  // Indexer for getting/setting items (equivalent to Python __getitem__ and __setitem__)
  public Chord this[int index]
  {
    get => _chords[index];
    set => _chords[index] = value;
  }

  // Equality comparison (equivalent to Python __eq__)
  public override bool Equals(object obj)
  {
    if (!(obj is Progression other))
      throw new ArgumentException($"Cannot compare Progression with {obj?.GetType().Name ?? "null"}");

    if (Count != other.Count)
      return false;

    return _chords.Zip(other._chords, (c, o) => c.Equals(o)).All(equal => equal);
  }

  public override int GetHashCode()
  {
    return _chords.Aggregate(0, (hash, chord) => hash ^ chord.GetHashCode());
  }

  public static bool operator ==(Progression left, Progression right) => left?.Equals(right) ?? right is null;
  public static bool operator !=(Progression left, Progression right) => !(left == right);

  /// <summary>
  /// Appends a chord to the progression.
  /// </summary>
  /// <param name="chord">The chord to append (string or Chord).</param>
  public void Append(object chord)
  {
    _chords.Add(AsChord(chord));
  }

  /// <summary>
  /// Inserts a chord at the specified index.
  /// </summary>
  /// <param name="index">The index to insert at.</param>
  /// <param name="chord">The chord to insert (string or Chord).</param>
  public void Insert(int index, object chord)
  {
    _chords.Insert(index, AsChord(chord));
  }

  /// <summary>
  /// Removes and returns a chord from the progression.
  /// </summary>
  /// <param name="index">The index of the chord to pop (default: -1, last item).</param>
  /// <returns>The removed Chord.</returns>
  public Chord Pop(int index = -1)
  {
    if (index < 0)
      index = _chords.Count + index; // Convert negative index to positive
    if (index < 0 || index >= _chords.Count)
      throw new ArgumentOutOfRangeException(nameof(index), "Index out of range");
    var chord = _chords[index];
    _chords.RemoveAt(index);
    return chord;
  }

  /// <summary>
  /// Transposes all chords in the progression by a number of semitones.
  /// </summary>
  /// <param name="trans">The number of semitones to transpose.</param>
  public void Transpose(int trans)
  {
    foreach (var chord in _chords)
    {
      chord.Transpose(trans);
    }
  }

  /// <summary>
  /// Converts an input to a Chord instance.
  /// </summary>
  /// <param name="chord">Chord name (string) or Chord instance.</param>
  /// <returns>A Chord instance.</returns>
  private static Chord AsChord(object chord)
  {
    return chord switch
    {
      Chord c => c,
      string s => new Chord(s),
      _ => throw new ArgumentException("Input type should be string or Chord instance.")
    };
  }
}