using System.Collections.ObjectModel;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GlobExpressions;

namespace J6ChordSearcher
{
  public partial class MainWindow : Window
  {
    private List<ChordSet> chordSets;
    private ObservableCollection<TransposedChordSet> searchResults;

    // Maps note names to chromatic indices (0–11)
    private readonly Dictionary<string, int> noteToIndex = new()
    {
      { "C", 0 }, { "C#", 1 }, { "Db", 1 }, { "D", 2 }, { "D#", 3 }, { "Eb", 3 }, { "E", 4 },
      { "F", 5 }, { "F#", 6 }, { "Gb", 6 }, { "G", 7 }, { "G#", 8 }, { "Ab", 8 }, { "A", 9 },
      { "A#", 10 }, { "Bb", 10 }, { "B", 11 }
    };

    // Preferred note names for transposition output (using sharps)
    private readonly string[] noteNames = ["C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"];

    public MainWindow()
    {
      InitializeComponent();

      // Initialize chord sets with sample data
      chordSets = InitializeChordSets();
      searchResults = [];
      listBoxResults.ItemsSource = searchResults;
    }

    private List<ChordSet> InitializeChordSets()
    {
      // Sample chord sets (expand this list as needed)
      return
      [
        new()
        {
          Number = 1,
          Name = "Pop",
          Chords =
          [
            "Cadd9",
            "C#M9/C",
            "Dm7",
            "D#M7",
            "Cadd9/E",
            "FM9",
            "Dadd9/F#",
            "Em7/G",
            "Fm6/G#",
            "FM/A",
            "Gm/A#",
            "G/B"
          ]
        },

        new()
        {
          Number = 2,
          Name = "Pop",
          Chords =
          [
            "CM9",
            "C#dim7",
            "Dm9",
            "D# dim7",
            "Em7",
            "FM9",
            "F#m7b5",
            "F/A",
            "G# dim7",
            "Am9",
            "C/A#",
            "Bm7b5"
          ]
        },

        new()
        {
          Number = 3,
          Name = "Jazz",
          Chords =
          [
            "D7sus2/C",
            "C#7#9",
            "Dm9",
            "D7#9",
            "E7#9",
            "FM9",
            "F#7#9",
            "G7(13)",
            "G#7(13)",
            "Am7(11)",
            "A#9",
            "Bm7(11)"
          ]
        },

        new()
        {
          Number = 4,
          Name = "Jazz",
          Chords =
          [
            "Dsus2/C",
            "C#7#9",
            "Dm9",
            "D#9",
            "Em9",
            "Fm9",
            "F#m7b5",
            "Gaug7",
            "G#7(13)",
            "Aaug7",
            "A#7(13)",
            "Bm7(11)"
          ]
        },

        new()
        {
          Number = 5,
          Name = "Jazz",
          Chords =
          [
            "CM9",
            "C#M7",
            "DM9",
            "D#M9",
            "EM9",
            "FM9",
            "F#M9",
            "GM9",
            "G#M9",
            "AM9",
            "A#M9",
            "BM9"
          ]
        },

        new()
        {
          Number = 6,
          Name = "Blues",
          Chords =
          [
            "C9",
            "C#9",
            "D9",
            "D#9",
            "E7#9",
            "Fm9",
            "F#dim7",
            "G7(13)",
            "G#dim7",
            "Aaug7",
            "A#7(13)",
            "Bm7b5"
          ]
        },

        new()
        {
          Number = 7,
          Name = "Trad Maj",
          Chords =
          [
            "C",
            "C#dim7",
            "Dm",
            "D#dim7",
            "Em",
            "F",
            "F#m7b5",
            "G",
            "G#dim7",
            "Am",
            "A#",
            "Bdim"
          ]
        },

        new()
        {
          Number = 7,
          Name = "Trad Maj",
          Chords =
          [
            "C",
            "C#dim7",
            "Dm",
            "D#dim7",
            "Em",
            "F",
            "F#m7b5",
            "G",
            "G#dim7",
            "Am",
            "A#",
            "Bdim"
          ]
        },

        new()
        {
          Number = 8,
          Name = "Trad Min",
          Chords =
          [
            "Cm",
            "C#",
            "Ddim",
            "Eb",
            "Edim7",
            "Fm",
            "F#dim7",
            "Gm",
            "G#",
            "Am7b5",
            "A#",
            "Bdim7"
          ]
        },

        new()
        {
          Number = 9,
          Name = "Trad Min 2",
          Chords =
          [
            "Cm",
            "C#",
            "Ddim",
            "D#aug",
            "Em",
            "Fm",
            "F#dim7",
            "G",
            "G#",
            "Am7b5",
            "A#",
            "Bdim"
          ]
        },

        new()
        {
          Number = 10,
          Name = "Pop Min",
          Chords =
          [
            "Cmadd9",
            "C#M7",
            "Dm7b5",
            "D#M7",
            "Edim7",
            "Fm9",
            "F#dim7",
            "Gm7",
            "G#M7",
            "Am7b5",
            "G#/A#",
            "Bdim7"
          ]
        },

        new()
        {
          Number = 11,
          Name = "Pop Min",
          Chords =
          [
            "Cmadd9",
            "Gdim/C#",
            "Dm7b5",
            "D#M7",
            "EM9",
            "Fm9",
            "F#dim7",
            "Gaug7",
            "G#M7",
            "Am7b5",
            "Cm7/A#",
            "Baug#9"
          ]
        },

        new()
        {
          Number = 12,
          Name = "Jazz Min",
          Chords =
          [
            "Cm7(11)",
            "C#7#9",
            "Dm7b5",
            "D#M7#5",
            "E9",
            "F9",
            "F#dim7",
            "G7#9",
            "G#M7b5",
            "Am7b5",
            "A#m7",
            "Bdim7"
          ]
        },

        new()
        {
          Number = 13,
          Name = "Jazz Min",
          Chords =
          [
            "Cm9",
            "C#9",
            "Dm9",
            "D#9",
            "EM9",
            "Fm9",
            "F#dim7",
            "G7(13)",
            "G#m6",
            "Am7b5",
            "A#m7",
            "Bm7b5"
          ]
        },

        new()
        {
          Number = 17,
          Name = "Utility",
          Chords =
          [
            "C",
            "C#",
            "D",
            "D#",
            "E",
            "F",
            "F#",
            "G",
            "G#",
            "A",
            "A#",
            "B"
          ]
        },

        new()
        {
          Number = 18,
          Name = "Utility",
          Chords =
          [
            "Cm",
            "C#m",
            "Dm",
            "D#m",
            "E",
            "Fm",
            "F#m",
            "Gm",
            "G#m",
            "Am",
            "A#m",
            "Bm"
          ]
        },

        new()
        {
          Number = 19,
          Name = "Utility",
          Chords =
          [
            "CM7",
            "C#M7",
            "DM7",
            "D#M7",
            "EM7",
            "FM7",
            "F#M7",
            "GM7",
            "G#M7",
            "AM7",
            "A#M7",
            "BM7"
          ]
        },

        new()
        {
          Number = 20,
          Name = "Utility",
          Chords =
          [
            "Cm7",
            "C#m7",
            "Dm7",
            "D#m7",
            "Em7",
            "Fm7",
            "F#m7",
            "Gm7",
            "G#m7",
            "Am7",
            "A#m7",
            "Bm7"
          ]
        },

        new()
        {
          Number = 21,
          Name = "Utility",
          Chords =
          [
            "CM9",
            "C#M9",
            "DM9",
            "D#M9",
            "EM9",
            "FM9",
            "F#M9",
            "GM9",
            "G#M9",
            "AM9",
            "A#M9",
            "BM9"
          ]
        },

        new()
        {
          Number = 22,
          Name = "Utility",
          Chords =
          [
            "Cm9",
            "C#m9",
            "Dm9",
            "D#m9",
            "Em9",
            "Fm9",
            "F#m9",
            "Gm9",
            "G#m9",
            "Am9",
            "A#m9",
            "Bm9"
          ]
        },

        new()
        {
          Number = 23,
          Name = "Utility",
          Chords =
          [
            "CM9/#11",
            "C#M9/#11",
            "DM9/#11",
            "D#M9/#11",
            "EM9/#11",
            "FM9/#11",
            "F#M9/#11",
            "GM9/#11",
            "G#M9/#11",
            "AM9/#11",
            "A#M9/#11",
            "BM9/#11"
          ]
        },

        new()
        {
          Number = 24,
          Name = "Utility",
          Chords =
          [
            "Cm9/11",
            "C#m9/11",
            "Dm9/11",
            "D#m9/11",
            "Em9/11",
            "Fm9/11",
            "F#m9/11",
            "Gm9/11",
            "G#m9/11",
            "Am9/11",
            "A#m9/11",
            "Bm9/11"
          ]
        },

        new()
        {
          Number = 26,
          Name = "Utility",
          Chords =
          [
            "Cm7",
            "Cm7/D#",
            "Cm7/G",
            "Cm7/A#",
            "Cm7",
            "Cm7/D#",
            "Cm7",
            "Cm7/G",
            "Cm7",
            "Cm7/D#",
            "Cm7/G",
            "Cm7/A#"
          ]
        },

        new()
        {
          Number = 27,
          Name = "Pop/Synth",
          Chords =
          [
            "C",
            "Em",
            "G",
            "Am",
            "Bm",
            "C",
            "Em",
            "G",
            "Am",
            "Bm",
            "C",
            "Em"
          ]
        },

        new()
        {
          Number = 28,
          Name = "Pop",
          Chords =
          [
            "C",
            "C7",
            "Dm7",
            "D#M7",
            "C/E",
            "F",
            "Fm",
            "G",
            "C/G",
            "Am7",
            "Eaug/A#",
            "G7/B"
          ]
        },


        new()
        {
          Number = 28,
          Name = "Pop",
          Chords =
          [
            "C",
            "C7",
            "Dm7",
            "D#M7",
            "C/E",
            "F",
            "Fm",
            "G",
            "C/G",
            "Am7",
            "Eaug/A#",
            "G7/B"
          ]
        },

        new()
        {
          Number = 29,
          Name = "Pop",
          Chords =
          [
            "C",
            "FM7",
            "G",
            "Em7",
            "Dm7",
            "CM7/E",
            "F",
            "D7/G",
            "G",
            "Am",
            "Dm",
            "G7"
          ]
        },

        new()
        {
          Number = 30,
          Name = "Pop",
          Chords =
          [
            "Cm",
            "D#",
            "G#",
            "A#",
            "Gm",
            "G#",
            "D#",
            "A#sus4/D",
            "Cm",
            "G/B",
            "G#",
            "F/A"
          ]
        },

        new()
        {
          Number = 31,
          Name = "Pop",
          Chords =
          [
            "Cadd11",
            "Bb/C",
            "Dm7",
            "D7",
            "Cadd9/E",
            "FM7",
            "F7",
            "Gm7",
            "A/G",
            "FM7/A",
            "F/Bb",
            "G7/B"
          ]
        },

        new()
        {
          Number = 32,
          Name = "Pop",
          Chords =
          [
            "Cmb13",
            "D#M7",
            "G7/D",
            "A#/D#",
            "C7",
            "Fm7",
            "D#",
            "Gm7",
            "G#M9",
            "G#m6",
            "F7/A",
            "A#add11"
          ]
        },

        new()
        {
          Number = 33,
          Name = "Cinematic",
          Chords =
          [
            "CM7",
            "F/E",
            "A#M7",
            "G",
            "Dm7",
            "C",
            "A#M7",
            "G",
            "C",
            "A/C#",
            "Dm",
            "G/F"
          ]
        },

        new()
        {
          Number = 34,
          Name = "Cinematic/Synthwave",
          Chords =
          [
            "Csus2",
            "Dsus2",
            "D#sus2",
            "Fsus2",
            "Gsus2",
            "A#sus2",
            "Csus2",
            "Dsus2",
            "D#sus2",
            "Fsus2",
            "Gsus2",
            "A#sus2"
          ]
        },

        new()
        {
          Number = 35,
          Name = "Cinematic/House",
          Chords =
          [
            "CM7",
            "Am7",
            "DM7",
            "Bm7",
            "EM7",
            "C#m7",
            "F#M7",
            "D#m7",
            "G#M7",
            "Fm7",
            "A#M7",
            "Gm7"
          ]
        },

        new()
        {
          Number = 36,
          Name = "Cinematic",
          Chords =
          [
            "Ebsus2/C",
            "Fsus2/D",
            "Gsus2/E",
            "Absus2/F",
            "Bbsus2/G",
            "Csus2/A",
            "Dsus2/B",
            "Ebsus2/C",
            "Fsus2/D",
            "Gsus2/E",
            "Absus2/F",
            "Bbsus2/G"
          ]
        }

      ];
    }

    private void SearchButton_Click(object sender, RoutedEventArgs e)
    {
      searchResults.Clear();

      // Collect non-empty search terms from TextBoxes
      List<string> searchTerms = [];
      foreach (var textBox in FindVisualChildren<TextBox>(this))
      {
        string trimmedText = textBox.Text.Trim();
        if (!string.IsNullOrWhiteSpace(trimmedText))
        {
          searchTerms.Add(trimmedText);
        }
      }

      if (searchTerms.Count != 0)
      {
        foreach (var chordSet in chordSets)
        {
          for (int k = 0; k < 12; k++) // Check all 12 transpositions
          {
            var transposedChords = chordSet.Chords.Select(chord => TransposeChord(chord, k)).ToList();
            // Check if all search terms match at least one chord
            if (searchTerms.All(term =>
                  transposedChords.Any(chord => IsMatch(chord, term))))
            {
              var transposedSet = new TransposedChordSet
              {
                OriginalSet = chordSet,
                Transposition = k,
                TransposedChords = transposedChords
              };
              searchResults.Add(transposedSet);
            }
          }
        }
      }

      // Search all chord sets and their transpositions
    }

    private static bool IsMatch(string chord, string term)
    {
      return new Glob(term).IsMatch(chord);
    }

    private string TransposeChord(string chord, int k)
    {
      string root = GetRoot(chord);
      int index = noteToIndex[root];
      int newIndex = (index + k) % 12; // Modulo 12 for chromatic scale
      string newRoot = noteNames[newIndex];
      string quality = chord.Substring(root.Length);
      return newRoot + quality;
    }

    private string GetRoot(string chord)
    {
      if (chord.Length >= 2 && noteToIndex.ContainsKey(chord.Substring(0, 2)))
      {
        return chord.Substring(0, 2); // e.g., "C#", "Bb"
      }
      else if (chord.Length >= 1 && noteToIndex.ContainsKey(chord.Substring(0, 1)))
      {
        return chord.Substring(0, 1); // e.g., "C", "A"
      }

      throw new ArgumentException("Invalid chord name");
    }

    // Helper to find all TextBoxes
    private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
      if (depObj != null)
      {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
          DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
          if (child is T tChild)
          {
            yield return tChild;
          }

          foreach (T childOfChild in FindVisualChildren<T>(child))
          {
            yield return childOfChild;
          }
        }
      }
    }
  }

  public class ChordSet
  {
    public int Number { get; set; }
    public string Name { get; set; }
    public List<string> Chords { get; set; }
  }

  public class TransposedChordSet
  {
    public ChordSet OriginalSet { get; set; }
    public int Transposition { get; set; }
    public List<string> TransposedChords { get; set; }

    public override string ToString()
    {
      string chords = string.Join(", ", TransposedChords);
      return $"{OriginalSet.Name} +{Transposition}: {chords}";
    }
  }
}

public class TransposerConfig
{
  public string Sharp = "#";
  public string Flat = "b";
  public string Abc = "abc";
  public string Doremi = "doremi";

  // Regular Expression Patterns
  public string GetKeyRegexAbc()
  {
    return "[ABCDEFG][" + Sharp + Flat + "]{0,2}";
  }

  public string GetKeyRegexDoremi()
  {
    return "(?:DO|RE|MI|FA|SOL|LA|SI|DO)[" + Sharp + Flat + "]{0,2}";
  }

  public Regex GetChordRegex()
  {
    return new Regex("((?:" + GetKeyRegexDoremi() + ")|(?:" + GetKeyRegexAbc() + "))");
  }

  public Regex GetChordGroupRegex(string preChord, string postChord)
  {
    return new Regex("(" + Regex.Escape(preChord) + ")((?:(?!" + Regex.Escape(postChord) + ").)*)(" + Regex.Escape(postChord) + ")");
  }

  // Standard Keys
  public List<string> SharpFlat()
  {
    return [Sharp, Flat];
  }

  public List<string> ReferenceAbcKeys()
  {
    return ["C", "C" + Sharp, "D", "E" + Flat, "E", "F", "F" + Sharp, "G", "G" + Sharp, "A", "B" + Flat, "B"];
  }

  public List<string> ReferenceDoremiKeys()
  {
    return
      ["DO", "DO" + Sharp, "RE", "MI" + Flat, "MI", "FA", "FA" + Sharp, "SOL", "SOL" + Sharp, "LA", "SI" + Flat, "SI"];
  }

  // Standardizing Keys/Chords
  public string KeyToReferenceAbc(string key)
  {
    var keys = new Dictionary<string, string>
        {
            {"C", "C"}, {"D", "D"}, {"E", "E"}, {"F", "F"}, {"G", "G"}, {"A", "A"}, {"B", "B"},
            {"C" + Sharp, "C" + Sharp}, {"D" + Sharp, "E" + Flat}, {"E" + Sharp, "F"}, {"F" + Sharp, "F" + Sharp},
            {"G" + Sharp, "G" + Sharp}, {"A" + Sharp, "B" + Flat}, {"B" + Sharp, "C"},
            {"C" + Flat, "B"}, {"D" + Flat, "C" + Sharp}, {"E" + Flat, "E" + Flat}, {"F" + Flat, "E"},
            {"G" + Flat, "F" + Sharp}, {"A" + Flat, "G" + Sharp}, {"B" + Flat, "B" + Flat},
            {"C" + Sharp + Sharp, "D"}, {"D" + Sharp + Sharp, "E"}, {"E" + Sharp + Sharp, "F" + Sharp},
            {"F" + Sharp + Sharp, "G"}, {"G" + Sharp + Sharp, "A"}, {"A" + Sharp + Sharp, "B"},
            {"B" + Sharp + Sharp, "C" + Sharp},
            {"C" + Flat + Flat, "B" + Flat}, {"D" + Flat + Flat, "C"}, {"E" + Flat + Flat, "D"},
            {"F" + Flat + Flat, "E" + Flat}, {"G" + Flat + Flat, "F"}, {"A" + Flat + Flat, "G"},
            {"B" + Flat + Flat, "A"}
        };
    if (keys.TryGetValue(key, out string referenceKey))
    {
      return referenceKey;
    }
    throw new ArgumentException($"Invalid key: {key}");
  }

  public string KeyToReferenceDoremi(string key)
  {
    var keys = new Dictionary<string, string>
        {
            {"DO", "DO"}, {"RE", "RE"}, {"MI", "MI"}, {"FA", "FA"}, {"SOL", "SOL"}, {"LA", "LA"}, {"SI", "SI"},
            {"DO" + Sharp, "DO" + Sharp}, {"RE" + Sharp, "MI" + Flat}, {"MI" + Sharp, "FA"}, {"FA" + Sharp, "FA" + Sharp},
            {"SOL" + Sharp, "SOL" + Sharp}, {"LA" + Sharp, "SI" + Flat}, {"SI" + Sharp, "DO"},
            {"DO" + Flat, "SI"}, {"RE" + Flat, "DO" + Sharp}, {"MI" + Flat, "MI" + Flat}, {"FA" + Flat, "MI"},
            {"SOL" + Flat, "FA" + Sharp}, {"LA" + Flat, "SOL" + Sharp}, {"SI" + Flat, "SI" + Flat},
            {"DO" + Sharp + Sharp, "RE"}, {"RE" + Sharp + Sharp, "MI"}, {"MI" + Sharp + Sharp, "FA" + Sharp},
            {"FA" + Sharp + Sharp, "SOL"}, {"SOL" + Sharp + Sharp, "LA"}, {"LA" + Sharp + Sharp, "SI"},
            {"SI" + Sharp + Sharp, "DO" + Sharp},
            {"DO" + Flat + Flat, "SI" + Flat}, {"RE" + Flat + Flat, "DO"}, {"MI" + Flat + Flat, "RE"},
            {"FA" + Flat + Flat, "MI" + Flat}, {"SOL" + Flat + Flat, "FA"}, {"LA" + Flat + Flat, "SOL"},
            {"SI" + Flat + Flat, "LA"}
        };
    if (keys.TryGetValue(key, out string referenceKey))
    {
      return referenceKey;
    }
    throw new ArgumentException($"Invalid key: {key}");
  }

  public string KeyToReference(string key)
  {
    if (Common.IsAbc(key))
    {
      return KeyToReferenceAbc(key);
    }
    else if (Common.IsDoremi(key))
    {
      return KeyToReferenceDoremi(key);
    }
    throw new ArgumentException($"Invalid key: {key}");
  }

  // Scales
  public List<string> KeyChordsAbc(string key)
  {
    var keys = new Dictionary<string, List<string>>
        {
            {"C", ["C", "C" + Sharp, "D", "E" + Flat, "E", "F", "F" + Sharp, "G", "A" + Flat, "A", "B" + Flat, "B"] },
            {"C" + Sharp,
              [
                "B" + Sharp, "C" + Sharp, "D", "D" + Sharp, "E", "E" + Sharp, "F" + Sharp, "G", "G" + Sharp, "A",
                "A" + Sharp, "B"
              ]
            },
            {"D" + Flat,
              [
                "C", "D" + Flat, "D", "E" + Flat, "F" + Flat, "F", "G" + Flat, "G", "A" + Flat, "B" + Flat + Flat,
                "B" + Flat, "C" + Flat
              ]
            },
            // Add other keys as needed...
        };
    if (keys.TryGetValue(key, out List<string> chords))
    {
      return chords;
    }
    throw new ArgumentException($"Invalid key: {key}");
  }

  public List<string> KeyChordsDoremi(string key)
  {
    string abcKey = Common.ChordDoremiToAbc(key);
    List<string> chords = KeyChordsAbc(abcKey);
    return chords.ConvertAll(ch => Common.ChordAbcToDoremi(ch));
  }

  public List<string> KeyChords(string key)
  {
    if (Common.IsAbc(key))
    {
      return KeyChordsAbc(key);
    }
    else if (Common.IsDoremi(key))
    {
      return KeyChordsDoremi(key);
    }
    throw new ArgumentException($"Invalid key: {key}");
  }
}

public static class Common
{
  private static readonly TransposerConfig config = new();

  public static readonly Dictionary<string, string> AbcToDoremiDictionary = new()
  {
        {"A", "LA"}, {"B", "SI"}, {"C", "DO"}, {"D", "RE"}, {"E", "MI"}, {"F", "FA"}, {"G", "SOL"}
    };

  public static readonly Dictionary<string, string> DoremiToAbcDictionary = new()
  {
        {"LA", "A"}, {"SI", "B"}, {"DO", "C"}, {"RE", "D"}, {"MI", "E"}, {"FA", "F"}, {"SOL", "G"}
    };

  public static bool IsAbc(string chord)
  {
    string cleanChord = Regex.Replace(chord, "[" + config.Sharp + config.Flat + "]", "");
    return AbcToDoremiDictionary.ContainsKey(cleanChord);
  }

  public static bool IsDoremi(string chord)
  {
    string cleanChord = Regex.Replace(chord, "[" + config.Sharp + config.Flat + "]", "");
    return DoremiToAbcDictionary.ContainsKey(cleanChord);
  }

  public static string ChordStyle(string chord)
  {
    if (IsAbc(chord))
    {
      return config.Abc;
    }
    else if (IsDoremi(chord))
    {
      return config.Doremi;
    }
    throw new ArgumentException($"Invalid chord: {chord}");
  }

  public static string ChordDoremiToAbc(string chord)
  {
    if (IsDoremi(chord))
    {
      MatchCollection sharpFlat = Regex.Matches(chord, "[" + config.Sharp + config.Flat + "]");
      string cleanChord = Regex.Replace(chord, "[" + config.Sharp + config.Flat + "]", "");
      string translatedChord = DoremiToAbcDictionary[cleanChord];
      foreach (Match sf in sharpFlat)
      {
        translatedChord += sf.Value;
      }
      return translatedChord;
    }
    throw new ArgumentException($"Invalid chord: {chord}");
  }

  public static string ChordAbcToDoremi(string chord)
  {
    if (IsAbc(chord))
    {
      MatchCollection sharpFlat = Regex.Matches(chord, "[" + config.Sharp + config.Flat + "]");
      string cleanChord = Regex.Replace(chord, "[" + config.Sharp + config.Flat + "]", "");
      string translatedChord = AbcToDoremiDictionary[cleanChord];
      foreach (Match sf in sharpFlat)
      {
        translatedChord += sf.Value;
      }
      return translatedChord;
    }
    throw new ArgumentException($"Invalid chord: {chord}");
  }

  public static string ChordToChordStyle(string chord, string chordStyleOut = "abc")
  {
    string currentStyle = ChordStyle(chord);
    if (chordStyleOut == currentStyle)
    {
      return chord;
    }
    else if (chordStyleOut == config.Abc)
    {
      return ChordDoremiToAbc(chord);
    }
    else if (chordStyleOut == config.Doremi)
    {
      return ChordAbcToDoremi(chord);
    }
    throw new ArgumentException($"Invalid output chord style: {chordStyleOut}");
  }
}

public class Transposer
{
  private static readonly TransposerConfig config = new();

  public static string SongKey(string song, int halfTones = 0, string preChord = @"\\\[", string postChord = @"\]", string chordStyleOut = "abc")
  {
    Regex chordGroupRegex = config.GetChordGroupRegex(preChord, postChord);
    MatchCollection matches = chordGroupRegex.Matches(song);
    if (matches.Count == 0)
    {
      return null;
    }
    string firstChordGroup = matches[0].Groups[2].Value;
    string firstChord = config.GetChordRegex().Matches(firstChordGroup)[0].Value;
    string referenceKey = config.KeyToReference(firstChord);
    string transposedReferenceKey = TransposeChord(referenceKey, halfTones, null, chordStyleOut);
    return Common.ChordToChordStyle(transposedReferenceKey, chordStyleOut);
  }

  public static string TransposeChord(string chord, int halfTones, string toKey = null, string chordStyleOut = "abc")
  {
    chord = config.KeyToReference(chord);
    List<string> referenceKeys = Common.IsAbc(chord) ? config.ReferenceAbcKeys() : config.ReferenceDoremiKeys();
    int currentChordIndex = referenceKeys.IndexOf(chord);
    int transposedChordIndex = (currentChordIndex + halfTones) % referenceKeys.Count;
    if (transposedChordIndex < 0) transposedChordIndex += referenceKeys.Count; // Handle negative indices
    string transposedChord = referenceKeys[transposedChordIndex];
    if (toKey != null)
    {
      return ExpressChordInKey(transposedChord, toKey, chordStyleOut);
    }
    return Common.ChordToChordStyle(transposedChord, chordStyleOut);
  }

  public static string ExpressChordInKey(string chord, string key, string chordStyleOut = "abc")
  {
    key = Common.ChordToChordStyle(key, chordStyleOut);
    chord = Common.ChordToChordStyle(chord, chordStyleOut);
    List<string> referenceKeys = chordStyleOut == config.Abc ? config.ReferenceAbcKeys() : config.ReferenceDoremiKeys();
    int idx = referenceKeys.IndexOf(chord);
    return config.KeyChords(key)[idx];
  }

  public static string TransposeChordGroup(string line, int halfTones, string toKey = null, string chordStyleOut = "abc")
  {
    int posDifference = 0;
    foreach (Match match in config.GetChordRegex().Matches(line))
    {
      int initialPos = match.Index + posDifference;
      int finalPos = match.Index + match.Length + posDifference;
      string chord = line.Substring(initialPos, match.Length);
      string transposedChord = TransposeChord(chord, halfTones, toKey, chordStyleOut);
      posDifference += transposedChord.Length - chord.Length;
      line = line.Substring(0, initialPos) + transposedChord + line.Substring(finalPos);
    }
    return line;
  }

  public static string ProcessKeyChange(string currentKey, string toKey, int halfTones = 0, string chordStyleOut = "abc")
  {
    Match numberFormatKeyChange = Regex.Match(toKey, @"(\+|-)([0-9]+)");
    if (numberFormatKeyChange.Success)
    {
      int offset = int.Parse(numberFormatKeyChange.Value);
      toKey = TransposeChord(currentKey, offset);
    }
    return TransposeChord(config.KeyToReference(toKey), halfTones, null, chordStyleOut);
  }

  public class SongSegment
  {
    public string Content { get; set; }
    public string Prepend { get; set; }
    public string ToKey { get; set; }
  }

  public static List<SongSegment> SongKeySegments(string song, string toKey, int halfTones = 0, bool clean = true,
      string chordStyleOut = "abc", string preKey = @"\\key\{", string postKey = @"\}")
  {
    Regex keyChangeRegex = new Regex("(" + Regex.Escape(preKey) + ")((?:(?!" + Regex.Escape(postKey) + ").)*)(" + Regex.Escape(postKey) + ")");
    MatchCollection keyChangeMatches = keyChangeRegex.Matches(song);
    if (keyChangeMatches.Count == 0)
    {
      return null;
    }

    List<SongSegment> songSegments = [];
    Match firstMatch = keyChangeMatches[0];
    string preKeyStr = firstMatch.Groups[1].Value;
    string postKeyStr = firstMatch.Groups[3].Value;

    songSegments.Add(new SongSegment
    {
      Content = song.Substring(0, firstMatch.Index),
      Prepend = "",
      ToKey = ProcessKeyChange(toKey, toKey, halfTones, chordStyleOut)
    });

    int idx = firstMatch.Index + firstMatch.Length;
    for (int i = 0; i < keyChangeMatches.Count - 1; i++)
    {
      string processedToKey = ProcessKeyChange(toKey, keyChangeMatches[i].Groups[2].Value, halfTones, chordStyleOut);
      string keyChangeSignalStr = clean ? "" : preKeyStr + processedToKey + postKeyStr;
      songSegments.Add(new SongSegment
      {
        Content = song.Substring(idx, keyChangeMatches[i + 1].Index - idx),
        Prepend = keyChangeSignalStr,
        ToKey = processedToKey
      });
      idx = keyChangeMatches[i + 1].Index + keyChangeMatches[i + 1].Length;
    }

    if (keyChangeMatches.Count > 0)
    {
      Match lastMatch = keyChangeMatches[keyChangeMatches.Count - 1];
      string processedToKey = ProcessKeyChange(toKey, lastMatch.Groups[2].Value, halfTones, chordStyleOut);
      string keyChangeSignalStr = clean ? "" : preKeyStr + processedToKey + postKeyStr;
      songSegments.Add(new SongSegment
      {
        Content = song.Substring(idx),
        Prepend = keyChangeSignalStr,
        ToKey = processedToKey
      });
    }

    return songSegments;
  }

  public static string TransposeSong(string song, int halfTones = 0, string toKey = null, string preChord = @"\\\[",
      string postChord = @"\]", string chordStyleOut = "abc", string preKey = @"\\key\{", string postKey = @"\}",
      bool cleanKeyChangeSignals = true)
  {
    string autoToKeyNoTranspose = SongKey(song, 0, preChord, postChord, chordStyleOut);
    var songSegments = SongKeySegments(song, autoToKeyNoTranspose, halfTones, cleanKeyChangeSignals, chordStyleOut, preKey, postKey);

    if (songSegments != null)
    {
      string result = "";
      foreach (var segment in songSegments)
      {
        result += segment.Prepend + TransposeSong(segment.Content, halfTones, segment.ToKey, preChord, postChord, chordStyleOut);
      }
      return result;
    }

    if (toKey == "auto")
    {
      toKey = SongKey(song, halfTones, preChord, postChord, chordStyleOut);
    }

    Regex chordGroupRegex = config.GetChordGroupRegex(preChord, postChord);
    return chordGroupRegex.Replace(song, match =>
        match.Groups[1].Value + TransposeChordGroup(match.Groups[2].Value, halfTones, toKey, chordStyleOut) + match.Groups[3].Value);
  }
}

public static class ChordConverter
{
  // Mapping of notes to their chromatic scale indices (0-11)
  private static readonly Dictionary<string, int> NoteToIndex = new()
  {
        {"C", 0}, {"C#", 1}, {"Db", 1}, {"D", 2}, {"D#", 3}, {"Eb", 3}, {"E", 4},
        {"F", 5}, {"F#", 6}, {"Gb", 6}, {"G", 7}, {"G#", 8}, {"Ab", 8}, {"A", 9},
        {"A#", 10}, {"Bb", 10}, {"B", 11}, {"Cb", 11}
    };

  // List of sharp notes in A-B-C notation for reconstruction
  private static readonly string[] SharpNotesAbc =
  [
    "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"
  ];

  /// <summary>
  /// Converts a chord with flat notes to its sharp equivalent, preserving the input notation style.
  /// </summary>
  /// <param name="chord">The input chord (e.g., "Ebm7", "MIb/FA#").</param>
  /// <returns>The chord with flats converted to sharps (e.g., "D#m7", "RE#/FA#").</returns>
  /// <exception cref="ArgumentException">Thrown if the chord format or notes are invalid.</exception>
  public static string ConvertFlatToSharp(string chord)
  {
    if (string.IsNullOrEmpty(chord))
      throw new ArgumentException("Chord cannot be null or empty.");

    // Determine the original notation style
    string originalStyle = Common.ChordStyle(chord);
    // Convert to A-B-C if in DO-RE-MI
    string abcChord = originalStyle == "doremi" ? Common.ChordDoremiToAbc(chord) : chord;

    // Regex to parse chord: root (e.g., "Eb"), quality (e.g., "m7"), optional bass (e.g., "/Gb")
    Regex chordRegex = new Regex(@"^([A-G][#b]?)(.*)(/[A-G][#b]?)?$");
    Match match = chordRegex.Match(abcChord);
    if (!match.Success)
      throw new ArgumentException($"Invalid chord format: {abcChord}");

    // Extract components
    string root = match.Groups[1].Value;
    string quality = match.Groups[2].Value;
    string bass = match.Groups[3].Value;

    // Convert root note to sharp
    if (!NoteToIndex.TryGetValue(root, out int rootIndex))
      throw new ArgumentException($"Invalid root note: {root}");
    string newRoot = SharpNotesAbc[rootIndex];

    // Handle bass note if present
    string newBass = "";
    if (!string.IsNullOrEmpty(bass))
    {
      string bassNote = bass.Substring(1); // Remove "/"
      if (!NoteToIndex.TryGetValue(bassNote, out int bassIndex))
        throw new ArgumentException($"Invalid bass note: {bassNote}");
      newBass = "/" + SharpNotesAbc[bassIndex];
    }

    // Reconstruct the chord
    string newChord = newRoot + quality + newBass;

    // Convert back to DO-RE-MI if original was DO-RE-MI
    if (originalStyle == "doremi")
      newChord = Common.ChordAbcToDoremi(newChord);

    return newChord;
  }
}