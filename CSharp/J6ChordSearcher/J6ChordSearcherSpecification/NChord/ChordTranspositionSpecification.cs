using J6ChordSearcher.NChord;

namespace J6ChordSearcherSpecification.NChord;

[TestFixture]
public class ChordTranspositionSpecification
{
  [TestCase("C", 0, "C")]
  [TestCase("C", 2, "D")]
  [TestCase("C", -2, "A#")]
  [TestCase("G", 5, "C")]
  [TestCase("Cm", 3, "D#m")]
  [TestCase("C7", 1, "C#7")]
  [TestCase("C/G", 2, "D/A")]
  [TestCase("D#7/G#", 1, "E7/A")]
  [TestCase("Dadd9/F#", -1, "C#add9/F")]
  [TestCase("C7b9", 4, "E7b9")]
  [TestCase("F#", 6, "C")]
  [TestCase("Bb", -3, "G")]
  [TestCase("A#m7", 7, "Fm7")]
  [TestCase("Cdim", 3, "D#dim")]
  [TestCase("Caug", -4, "G#aug")]
  [TestCase("C", 12, "C")]
  [TestCase("C", -12, "C")]
  [TestCase("Db", 1, "D")]
  [TestCase("B", 1, "C")]
  [TestCase("E", -1, "D#")]
  [TestCase("C", 0, "C")]
  [TestCase("C", 2, "D")]
  [TestCase("C", -2, "A#")]
  [TestCase("G", 5, "C")]
  [TestCase("Cm", 3, "D#m")]
  [TestCase("C7", 1, "C#7")]
  [TestCase("C/G", 2, "D/A")]
  [TestCase("D#7/G#", 1, "E7/A")]
  [TestCase("Dadd9/F#", -1, "C#add9/F")]
  [TestCase("C7b9", 4, "E7b9")]
  [TestCase("F#", 6, "C")]
  [TestCase("Bb", -3, "G")]
  [TestCase("A#m7", 7, "Fm7")]
  [TestCase("Cdim", 3, "D#dim")]
  [TestCase("Caug", -4, "G#aug")]
  [TestCase("C", 12, "C")]
  [TestCase("C", -12, "C")]
  [TestCase("Db", 1, "D")]
  [TestCase("B", 1, "C")]
  [TestCase("E", -1, "D#")]
  public void TransposeChord1(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  [TestCase("D", 3, "F")]
  [TestCase("E", -3, "C#")]
  [TestCase("F", 4, "A")]
  [TestCase("G", -5, "D")]
  [TestCase("A", 2, "B")]
  [TestCase("B", -2, "A")]
  [TestCase("C#", 1, "D")]
  [TestCase("D#", -1, "D")]
  [TestCase("F#", 3, "A")]
  [TestCase("G#", -4, "E")]
  [TestCase("A#", 5, "D#")]
  [TestCase("C", -1, "B")]
  public void ShouldTransposeMajorChords(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  // Test cases for minor chords
  [TestCase("Cm", -3, "Am")]
  [TestCase("Dm", 2, "Em")]
  [TestCase("Em", -2, "Dm")]
  [TestCase("Fm", 3, "G#m")]
  [TestCase("Gm", -3, "Em")]
  [TestCase("Am", 4, "C#m")]
  [TestCase("Bm", -4, "Gm")]
  [TestCase("C#m", 1, "Dm")]
  [TestCase("D#m", -1, "Dm")]
  [TestCase("F#m", 2, "G#m")]
  [TestCase("G#m", -2, "F#m")]
  [TestCase("A#m", 3, "C#m")]
  public void ShouldTransposeMinorChords(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  // Test cases for seventh chords
  [TestCase("CM7", 2, "DM7")]
  [TestCase("Dm7", -2, "Cm7")]
  [TestCase("E7", 3, "G7")]
  [TestCase("FM7", -3, "DM7")]
  [TestCase("G7", 4, "B7")]
  [TestCase("Am7", -4, "Fm7")]
  [TestCase("B7", 1, "C7")]
  [TestCase("C#7", -1, "C7")]
  [TestCase("D#7", 2, "F7")]
  [TestCase("F#7", -2, "E7")]
  [TestCase("G#7", 3, "B7")]
  [TestCase("A#7", -3, "G7")]
  public void ShouldTranpose7ThChords(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  // Test cases for extended chords
  [TestCase("C9", 2, "D9")]
  [TestCase("Dm9", -2, "Cm9")]
  [TestCase("E13", 3, "G13")]
  [TestCase("FM9", -3, "DM9")]
  [TestCase("G7b9", 4, "B7b9")]
  [TestCase("Am11", -4, "Fm11")]
  [TestCase("B7#9", 1, "C7#9")]
  [TestCase("C#7b13", -1, "C7b13")]
  [TestCase("D#7#11", 2, "F7#11")]
  [TestCase("G#7sus4", 3, "B7sus4")]
  [TestCase("A#7b5", -3, "G7b5")]
  public void ShouldTransposeExtendedChords(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  // Test cases for slash chords
  [TestCase("C/E", 2, "D/F#")]
  [TestCase("Dm/F", -2, "Cm/D#")]
  [TestCase("E/G#", 3, "G/B")]
  [TestCase("F/A", -3, "D/F#")]
  [TestCase("G/B", 4, "B/D#")]
  [TestCase("Am/C", -4, "Fm/G#")]
  [TestCase("B/D#", 1, "C/E")]
  [TestCase("D#/F#", 2, "F/G#")]
  [TestCase("F#/A#", -2, "E/G#")]
  [TestCase("A#/C#", -3, "G/A#")]
  public void ShouldTransposeSlashChords(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  // Test cases for altered chords
  [TestCase("Caug", 2, "Daug")]
  [TestCase("Ddim", -2, "Cdim")]
  [TestCase("Eaug7", 3, "Gaug7")]
  [TestCase("Fdim7", -3, "Ddim7")]
  [TestCase("Gaug9", 4, "Baug9")]
  [TestCase("Am7b5", -4, "Fm7b5")]
  [TestCase("B7#5", 1, "C7#5")]
  [TestCase("C#m7b5", -1, "Cm7b5")]
  [TestCase("F#7b9", -2, "E7b9")]
  [TestCase("G#7#9", 3, "B7#9")]
  [TestCase("A#7b13", -3, "G7b13")]
  public void ShouldTransposeSomeAlteredChords(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  // Test cases for sus chords
  [TestCase("Csus2", 2, "Dsus2")]
  [TestCase("Dsus4", -2, "Csus4")]
  [TestCase("Esus2", 3, "Gsus2")]
  [TestCase("Fsus4", -3, "Dsus4")]
  [TestCase("Gsus2", 4, "Bsus2")]
  [TestCase("Asus4", -4, "Fsus4")]
  [TestCase("Bsus2", 1, "Csus2")]
  [TestCase("C#sus4", -1, "Csus4")]
  [TestCase("D#sus2", 2, "Fsus2")]
  [TestCase("F#sus4", -2, "Esus4")]
  [TestCase("G#sus2", 3, "Bsus2")]
  [TestCase("A#sus4", -3, "Gsus4")]
  public void ShouldTransposeSusChords(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  // Test cases for add chords
  [TestCase("Cadd9", 2, "Dadd9")]
  [TestCase("Dadd11", -2, "Cadd11")]
  [TestCase("Eadd13", 3, "Gadd13")]
  [TestCase("Fadd9", -3, "Dadd9")]
  [TestCase("Gadd11", 4, "Badd11")]
  [TestCase("Aadd13", -4, "Fadd13")]
  [TestCase("Badd9", 1, "Cadd9")]
  [TestCase("C#add11", -1, "Cadd11")]
  [TestCase("D#add13", 2, "Fadd13")]
  [TestCase("F#add9", -2, "Eadd9")]
  [TestCase("G#add11", 3, "Badd11")]
  [TestCase("A#add13", -3, "Gadd13")]
  public void TransposeChord9(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  // Test cases for complex slash chords
  [TestCase("C7/E", 2, "D7/F#")]
  [TestCase("Dm7/F", -2, "Cm7/D#")]
  [TestCase("E7/G#", 3, "G7/B")]
  [TestCase("FM7/A", -3, "DM7/F#")]
  [TestCase("G7/B", 4, "B7/D#")]
  [TestCase("Am7/C", -4, "Fm7/G#")]
  [TestCase("B7/D#", 1, "C7/E")]
  [TestCase("D#7/F#", 2, "F7/G#")]
  [TestCase("F#7/A#", -2, "E7/G#")]
  [TestCase("A#7/C#", -3, "G7/A#")]
  public void TransposeChord10(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  // Test cases for extreme transpositions
  [TestCase("C", 24, "C")]
  [TestCase("C", -24, "C")]
  [TestCase("D", 36, "D")]
  [TestCase("D", -36, "D")]
  [TestCase("E", 48, "E")]
  [TestCase("E", -48, "E")]
  [TestCase("F", 60, "F")]
  [TestCase("F", -60, "F")]
  [TestCase("G", 72, "G")]
  [TestCase("G", -72, "G")]
  [TestCase("A", 84, "A")]
  [TestCase("A", -84, "A")]
  [TestCase("B", 96, "B")]
  [TestCase("B", -96, "B")]
  public void TransposeChord11(string chord, int semitones, string expected)
  {
    var chord1 = new Chord(chord);
    chord1.Transpose(semitones, "C#");
    AssertChordTransposition(chord, chord1, semitones, expected);
  }

  private static void AssertChordTransposition(string chord, Chord transposedChord, int semitones, string expected)
  {
    Assert.That(transposedChord.ChordName, Is.EqualTo(new Chord(expected).ChordName),
      $"{chord} by {semitones} should be {expected} but got {transposedChord.ChordName}");
  }

}