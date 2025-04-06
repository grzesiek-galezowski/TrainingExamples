namespace J6ChordSearcher.Logic;

public class TransposedChordSet
{
  public ChordSet OriginalSet { get; set; }
  public int Transposition { get; set; }
  public List<string> TransposedChords { get; set; }

  public override string ToString()
  {
    var chords = string.Join(", ", TransposedChords.Select((c, i) => new List<int> {1,3,6,8,10}.Contains(i) ? $"[{c}]" : c));
    return $"{OriginalSet.Number}: {OriginalSet.Name} +{Transposition}: {chords}";
  }
}