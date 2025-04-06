using System.Collections.ObjectModel;
using System.Runtime.Intrinsics.X86;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GlobExpressions;
using J6ChordSearcher.Logic;
using J6ChordSearcher.NChord;
using LanguageExt.ClassInstances.Const;

namespace J6ChordSearcher;

public partial class MainWindow : Window
{
  private readonly List<ChordSet> chordSets;
  private readonly ObservableCollection<TransposedChordSet> searchResults;

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
          "D#dim7",
          "Em7",
          "FM9",
          "F#m7b5",
          "F/A",
          "G#dim7",
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
          "CM9#11",
          "C#M9#11",
          "DM9#11",
          "D#M9#11",
          "EM9#11",
          "FM9#11",
          "F#M9#11",
          "GM9#11",
          "G#M9#11",
          "AM9#11",
          "A#M9#11",
          "BM9#11"
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
      },

      new()
      {
        Number = 37,
        Name = "Cinematic",
        Chords = new List<string>
        {
          "C6sus2",
          "Dsus2",
          "Emadd11",
          "Dadd9/F#",
          "Gadd9",
          "Am7/11",
          "Bm7",
          "C6",
          "D6",
          "Emadd9",
          "E7sus4",
          "Em7b5/F"
        }
      },
      new()
      {
        Number = 38,
        Name = "New Age/Cinematic",
        Chords = new List<string>
        {
          "C",
          "Gsus4",
          "G7sus4",
          "A#/D#",
          "C/E",
          "Gsus4/F",
          "D/F#",
          "C/G",
          "E/G#",
          "Am",
          "Fsus4",
          "G"
        }
      },
      new()
      {
        Number = 39,
        Name = "Synthwave",
        Chords = new List<string>
        {
          "C6sus4",
          "Gmadd9/D",
          "Edim11",
          "Fsus2/D",
          "BbM7",
          "Gmadd9",
          "Am/G",
          "C7/G",
          "A#M7",
          "Gm/A",
          "C7/A#",
          "A#m6"
        }
      },
      new()
      {
        Number = 40,
        Name = "Synthwave",
        Chords = new List<string>
        {
          "Cadd9",
          "Dadd9",
          "D#add9",
          "Fadd9",
          "Gadd9",
          "G#add9",
          "A#add9",
          "Cadd9",
          "Dadd9",
          "D#add9",
          "Fadd9",
          "Gadd9"
        }
      },
      new()
      {
        Number = 41,
        Name = "Synthwave",
        Chords = new List<string>
        {
          "C",
          "C#dim7",
          "Fsus2/D",
          "D#add9",
          "Csus2/E",
          "FM7",
          "F#dim",
          "Gsus4",
          "G#6",
          "Csus2/A",
          "Csus2/A#",
          "G7/B"
        }
      },
      new()
      {
        Number = 42,
        Name = "Synthwave",
        Chords = new List<string>
        {
          "C",
          "A#sus2/D#",
          "A#7/G#",
          "G/B",
          "C",
          "Fm",
          "F#dim7",
          "C",
          "G#add9",
          "F/A",
          "A#",
          "G7/B"
        }
      },
      new()
      {
        Number = 43,
        Name = "Synthwave",
        Chords = new List<string>
        {
          "Cm7",
          "A#/C",
          "D#/G#",
          "A#/G#",
          "D#/F",
          "A#/F",
          "G#add9",
          "A#",
          "G#M7",
          "Fsus2/A",
          "A#6",
          "Fm7/B"
        }
      },
      new()
      {
        Number = 44,
        Name = "Synthwave",
        Chords = new List<string>
        {
          "Cm7",
          "C#sus2/F",
          "Gm7",
          "D#",
          "Csus2/E",
          "Fm",
          "D#dim7",
          "G#sus2/G",
          "Cm7",
          "F7",
          "Fm7",
          "G5"
        }
      },
      new()
      {
        Number = 45,
        Name = "Synthwave",
        Chords = new List<string>
        {
          "Ab",
          "Fm",
          "Gm",
          "Ab",
          "Fm",
          "Bb",
          "Gm",
          "Cm",
          "Ab",
          "Fm",
          "Bb",
          "Cm"
        }
      },
      new()
      {
        Number = 46,
        Name = "Synthwave",
        Chords = new List<string>
        {
          "C",
          "D",
          "Em",
          "D",
          "G",
          "Am",
          "Bm",
          "C",
          "D",
          "Em",
          "D",
          "G"
        }
      },
      new()
      {
        Number = 47,
        Name = "Synthwave/House",
        Chords = new List<string>
        {
          "Cm7",
          "D#M7",
          "Dm7",
          "Fm7",
          "D#M7",
          "Gm7",
          "Fm7",
          "G#M7",
          "Gm7",
          "A#7",
          "G#M7",
          "C#/C"
        }
      },
      new()
      {
        Number = 48,
        Name = "Trance",
        Chords = new List<string>
        {
          "Cm",
          "Ab/C",
          "Bb/D",
          "Eb",
          "C/E",
          "Fm",
          "F",
          "Gm",
          "Ab",
          "F7/A",
          "Bb",
          "G7/B"
        }
      },
      new()
      {
        Number = 49,
        Name = "House",
        Chords = new List<string>
        {
          "Cm7",
          "C#m7",
          "Dm7",
          "EbM7",
          "Gm/E",
          "Eb/F",
          "D7/F#",
          "Gm7",
          "AbM7",
          "Ab/Bb",
          "BbM7",
          "Bm7b5"
        }
      },
      new()
      {
        Number = 50,
        Name = "House",
        Chords = new List<string>
        {
          "Cmaj7",
          "Dmaj7",
          "Em7",
          "Fmaj9",
          "Gmaj9",
          "Eadd9/G#",
          "Am7",
          "Bm7",
          "Cmaj9",
          "Dm7/9",
          "Em7",
          "Fmaj7"
        }
      },
      new()
      {
        Number = 51,
        Name = "House",
        Chords = new List<string>
        {
          "Cm7",
          "G#/C#",
          "Dm7",
          "A#/D#",
          "Em7",
          "C/F",
          "F#m7",
          "D/G",
          "G#m7",
          "E/A",
          "A#m7",
          "F#/B"
        }
      },
      new()
      {
        Number = 52,
        Name = "House",
        Chords = new List<string>
        {
          "Cm7",
          "G#M7",
          "Dm7",
          "A#M7",
          "Em7",
          "CM7",
          "F#m7",
          "DM7",
          "G#m7",
          "EM7",
          "A#m7",
          "F#M7"
        }
      },
      new()
      {
        Number = 53,
        Name = "House",
        Chords = new List<string>
        {
          "Cm7/11",
          "Cdim7",
          "C#M7b5",
          "Ddim7",
          "Cm",
          "C#M7",
          "CM7",
          "Em7",
          "FM7",
          "Fm6",
          "Csus2/E",
          "Fm"
        }
      },
      new()
      {
        Number = 54,
        Name = "House",
        Chords = new List<string>
        {
          "CM7",
          "Em7",
          "Dm7",
          "FM7",
          "D#M7",
          "Gm7",
          "FM7",
          "Am7",
          "Gm7",
          "A#M7",
          "Am7",
          "Bm7"
        }
      },
      new()
      {
        Number = 55,
        Name = "Jazz House",
        Chords = new List<string>
        {
          "CM13",
          "CM7#5",
          "Dm7b5",
          "G7",
          "Cadd9/E",
          "Fadd9",
          "FmAdd9",
          "G9",
          "E7/G#",
          "Am9/11",
          "Gm11/Bb",
          "CM7#5/G#"
        }
      },
      new()
      {
        Number = 56,
        Name = "Jazz House",
        Chords = new List<string>
        {
          "Cm7",
          "Db6",
          "Dm7",
          "Dm7b5",
          "EbM7",
          "Fm7",
          "Gb6",
          "Gm7",
          "Ab6",
          "AbM7",
          "Am7",
          "BbM7"
        }
      },
      new()
      {
        Number = 57,
        Name = "House/Techno",
        Chords = new List<string>
        {
          "C5b9",
          "DbM7",
          "Dm7",
          "Ebsus11",
          "EM7#11",
          "Fm9",
          "GbM7#11",
          "Gm7",
          "AbM7#11",
          "A7alt",
          "Bb5add9b13",
          "C7sus4"
        }
      },
      new()
      {
        Number = 58,
        Name = "Techno",
        Chords = new List<string>
        {
          "Cm7",
          "A#m/C#",
          "A#/D",
          "D#",
          "C/E",
          "Cm/F",
          "D/F#",
          "G",
          "G#M7",
          "Csus4/A",
          "D#/A#",
          "G/B"
        }
      },
      new()
      {
        Number = 59,
        Name = "EDM",
        Chords = new List<string>
        {
          "CM9",
          "C6",
          "Dm9",
          "Dm6",
          "EM9",
          "FM9",
          "F6",
          "GM9",
          "G6",
          "Am9",
          "Am6",
          "Bm9"
        }
      },
      new()
      {
        Number = 60,
        Name = "EDM",
        Chords = new List<string>
        {
          "CM13(no 3)",
          "EM9(no 3)",
          "DM13(no 3)",
          "F#M9(no 3)",
          "EM13(no 3)",
          "FM13(no 3)",
          "AM9(no 3)",
          "GM13(no 3)",
          "BM9(no 3)",
          "AM13(no 3)",
          "C#M9(no 3)",
          "BM13(no 3)"
        }
      },
      new()
      {
        Number = 61,
        Name = "EDM",
        Chords = new List<string>
        {
          "Csus9/13",
          "C6",
          "Dsus9/13",
          "D6",
          "Esus9/13",
          "Fsus9/13",
          "F6",
          "Gsus9/13",
          "G6",
          "Asus9/13",
          "A6",
          "Bsus9/13"
        }
      },
      new()
      {
        Number = 62,
        Name = "EDM",
        Chords = new List<string>
        {
          "CM13",
          "C#sus9",
          "DM13",
          "D#sus9",
          "EM13",
          "FM13",
          "F#sus9",
          "Gmaj13",
          "Absus9",
          "AM13",
          "Bbsus9",
          "BM13"
        }
      },
      new()
      {
        Number = 63,
        Name = "EDM",
        Chords = new List<string>
        {
          "CM7",
          "Dbm7",
          "Dm7",
          "Eb6",
          "Em7",
          "FM7",
          "Gbm7",
          "GM7",
          "Abm7",
          "Am7",
          "Bb6",
          "Bm7"
        }
      },
      new()
      {
        Number = 64,
        Name = "EDM",
        Chords = new List<string>
        {
          "C6",
          "AM7",
          "D6",
          "BM7",
          "E6",
          "F6",
          "DM7",
          "G6",
          "EM7",
          "A6",
          "GbM7",
          "B6"
        }
      },
      new()
      {
        Number = 65,
        Name = "Gospel/R&B",
        Chords = new List<string>
        {
          "G/C",
          "C#dim7",
          "Dm7b5",
          "D#dim7",
          "Em7",
          "Fm11",
          "F#dim7",
          "Gm7b13",
          "G#M9",
          "Am7b13",
          "A#m7add13",
          "Bm7b13"
        }
      },
      new()
      {
        Number = 66,
        Name = "Gospel/R&B",
        Chords = new List<string>
        {
          "Cm7b13",
          "C#M13",
          "Dm7b13",
          "D#m11",
          "D/E",
          "G#m/F",
          "F#6",
          "A#m/G",
          "G#m11",
          "AM9",
          "A#7b13",
          "F#/B"
        }
      },
      new()
      {
        Number = 67,
        Name = "Gospel/R&B",
        Chords = new List<string>
        {
          "D",
          "C/D",
          "D/E",
          "Dm/F",
          "Dsus2/F#",
          "Gadd9",
          "E/G#",
          "A",
          "A#dim7",
          "Bm7",
          "D/C",
          "C#dim"
        }
      },
      new()
      {
        Number = 68,
        Name = "Lofi R&B",
        Chords = new List<string>
        {
          "Cmadd9",
          "Abadd9/C",
          "Bb7/D",
          "EbM9",
          "Eb7b9",
          "Fm9",
          "GbM6/9",
          "Gm7",
          "Eb/Ab",
          "Fsus4/A",
          "Bbsus",
          "Bbsus4b9"
        }
      },
      new()
      {
        Number = 69,
        Name = "Lofi R&B",
        Chords = new List<string>
        {
          "Ab7/C",
          "C#m11",
          "DM9",
          "Bsus4/Eb",
          "EMadd2",
          "DM7#11",
          "F#m11",
          "G6",
          "Abm7",
          "AM7add6",
          "F#add9/A#",
          "BMadd4"
        }
      },
      new()
      {
        Number = 70,
        Name = "Funk",
        Chords = new List<string>
        {
          "C9",
          "C/D",
          "D7b9",
          "EbM7",
          "Em7b13",
          "Eb/F",
          "F#13",
          "G7",
          "Abm6addb13",
          "Am7",
          "Bm7",
          "B7"
        }
      },
      new()
      {
        Number = 71,
        Name = "Funk",
        Chords = new List<string>
        {
          "C7alt",
          "DbM7",
          "D7alt",
          "Eb7",
          "C7/E",
          "F7alt",
          "F11sus2",
          "Gm7b5",
          "Ab9",
          "Bb9sus",
          "Bb7add9",
          "C11sus2"
        }
      },
      new()
      {
        Number = 72,
        Name = "Neo Soul",
        Chords = new List<string>
        {
          "Cm9",
          "AbM7",
          "Dm7",
          "EbDimM7",
          "EbM7",
          "Fm7",
          "CM7#5",
          "Gm7",
          "G7#5",
          "AbM7",
          "Fm7b5/B",
          "Bb13/D"
        }
      },
      new()
      {
        Number = 73,
        Name = "Neo Soul",
        Chords = new List<string>
        {
          "Cm7",
          "C#dim7",
          "Fm7",
          "Fm7b5",
          "EbM7",
          "Cm7/Eb",
          "Edim",
          "AbM7",
          "Fm7",
          "AbM13",
          "Bb13",
          "Bb6"
        }
      },
      new()
      {
        Number = 74,
        Name = "Neo Soul",
        Chords = new List<string>
        {
          "Em7/D",
          "Gm7b5/C#",
          "FM7/E",
          "DMb7/D#",
          "GM7/F#",
          "Edim7",
          "Cadd13/E",
          "E",
          "Gaddb9/G#",
          "Am9",
          "Dm7",
          "Emb9/F"
        }
      },
      new()
      {
        Number = 75,
        Name = "Neo Soul",
        Chords = new List<string>
        {
          "CM7",
          "C#m7",
          "Dm7",
          "EbM7",
          "Em7",
          "FM7",
          "Gb7",
          "GM7",
          "AbM7",
          "Am7",
          "BbM7",
          "Bm7"
        }
      },
      new()
      {
        Number = 76,
        Name = "Neo-Soul",
        Chords = new List<string>
        {
          "CM7sus2",
          "Gadd9/B",
          "DM7sus2",
          "E6sus4/C#",
          "EM7sus2",
          "F#6sus4/D#",
          "F#M7sus2",
          "G#6sus4/F",
          "G#M7sus2",
          "A#6sus4/G",
          "A#M7sus2",
          "Fadd9/A"
        }
      },
      new()
      {
        Number = 77,
        Name = "Neo-Soul",
        Chords = new List<string>
        {
          "CM7",
          "C#m7",
          "DM7",
          "D#m7",
          "EM7",
          "Fm7",
          "F#M7",
          "Gm7",
          "AbM7",
          "Am7",
          "BbM7",
          "Bm7"
        }
      },
      new()
      {
        Number = 78,
        Name = "Neo-Soul",
        Chords = new List<string>
        {
          "C11sus",
          "A7/C#",
          "Dm7",
          "EbM7",
          "C7/E",
          "F6",
          "D7/F#",
          "Gdim7",
          "E7/G#",
          "F6/A",
          "Bb6",
          "Bm7b5"
        }
      },
      new()
      {
        Number = 79,
        Name = "Neo-Soul",
        Chords = new List<string>
        {
          "Ab/C",
          "Db",
          "Bb/D",
          "Ebm",
          "C/E",
          "Db/F",
          "Gb",
          "Eb/G",
          "Db/Ab",
          "F7/A",
          "Ebm/Bb",
          "B"
        }
      },
      new()
      {
        Number = 80,
        Name = "Neo-Soul",
        Chords = new List<string>
        {
          "Cm7b5",
          "Db7sus",
          "Bbadd2/D",
          "Ebm7",
          "EM7",
          "Fmb6",
          "GbM9",
          "Ebadd9/G",
          "Ab7sus",
          "AdimM7",
          "Bbm9",
          "Gb/B"
        }
      },
      new()
      {
        Number = 81,
        Name = "Neo-Soul",
        Chords = new List<string>
        {
          "C7alt",
          "DbM7add6",
          "Bb7/D",
          "Db/Eb",
          "Edim7",
          "Fm9",
          "Gb6/9",
          "Gm7b5",
          "Eb/Ab",
          "F7/A",
          "Bbm9",
          "BM7#11"
        }
      },
      new()
      {
        Number = 82,
        Name = "Jazz/Bossa",
        Chords = new List<string>
        {
          "Gm7",
          "Bb/Ab",
          "Am7",
          "C/Bb",
          "Bm7",
          "D/C",
          "C#m7",
          "E/D",
          "D#m7",
          "F#/E",
          "Fm7",
          "Ab/Gb"
        }
      },
      new()
      {
        Number = 83,
        Name = "Bossa Nova",
        Chords = new List<string>
        {
          "CM9",
          "C#dim",
          "Dm7",
          "D#dim",
          "CM9/E",
          "FM9",
          "F#dim",
          "Gm9",
          "C13b9",
          "F6",
          "BbM9",
          "G13b9"
        }
      },
      new()
      {
        Number = 84,
        Name = "Bossa Nova",
        Chords = new List<string>
        {
          "CM7",
          "C#Dim",
          "Dm11",
          "D#Dim",
          "Em11",
          "G/F",
          "F9#11",
          "Gsus13",
          "Abdim7",
          "FM7/A",
          "Bb13",
          "Eb/B"
        }
      },
      new()
      {
        Number = 85,
        Name = "Jazz",
        Chords = new List<string>
        {
          "CM7#11",
          "DbM7#11",
          "Dm9",
          "EbM7#11",
          "Em9",
          "FM7#11",
          "F#m11",
          "G6/9",
          "AbM7#11",
          "Am9",
          "BbM13",
          "Bm11b5"
        }
      },
      new()
      {
        Number = 86,
        Name = "Jazz",
        Chords = new List<string>
        {
          "CM9",
          "Db9#11",
          "Dm9",
          "D13b9/Eb",
          "Em11",
          "FM13",
          "E/F",
          "FM7/G",
          "Ab7#9#5",
          "Am9/11",
          "BbM9",
          "Bdim7"
        }
      },
      new()
      {
        Number = 87,
        Name = "Jazz",
        Chords = new List<string>
        {
          "Cmaj7",
          "Aadd9/C#",
          "Dm6",
          "B7/D#",
          "Em7",
          "Fmaj9",
          "Dadd9/F#",
          "Gm7/9",
          "Abmaj9",
          "Cadd9/G",
          "Eadd9/G#",
          "Asus7"
        }
      },
      new()
      {
        Number = 88,
        Name = "Jazz",
        Chords = new List<string>
        {
          "Fmaj7/9",
          "Gm7b5",
          "Abmaj7/9",
          "Bbm7b5",
          "Bmaj7/9",
          "C#m7b5",
          "DM7/9",
          "Em7b5",
          "CM7/9",
          "Dm7b5/9",
          "GbM7/9",
          "Abm7b5"
        }
      },
      new()
      {
        Number = 89,
        Name = "Jazz",
        Chords = new List<string>
        {
          "Cadd9",
          "C#dim7",
          "Gadd13/B",
          "D#dim#5",
          "FM7/E",
          "G7sus2/F",
          "D#dim7",
          "C6",
          "Ddim7",
          "Am/C",
          "Dm7b5",
          "G7/D"
        }
      },
      new()
      {
        Number = 90,
        Name = "Jazz",
        Chords = new List<string>
        {
          "Em7",
          "Edim7",
          "FM7",
          "F#dim7",
          "Em7",
          "C6/E",
          "F#dim7",
          "FM7b5",
          "Eb7/F",
          "Gadd9",
          "A#11/F",
          "F#dim7"
        }
      },
      new()
      {
        Number = 91,
        Name = "Jazz",
        Chords = new List<string>
        {
          "Gm7",
          "G#M7",
          "Am7",
          "A#M7",
          "G/B",
          "D#M7",
          "A#/D",
          "D",
          "CM7sus2",
          "Em7",
          "F6",
          "C6sus2b5"
        }
      },
      new()
      {
        Number = 92,
        Name = "Jazz",
        Chords = new List<string>
        {
          "A#M7",
          "BM7",
          "Cm7b5",
          "C#dim7",
          "Em7b5/D",
          "Fm7/D#",
          "Edim7",
          "F7/D#",
          "F#dim7",
          "Gm7/F",
          "G#M7/D#",
          "F7/D#"
        }
      },
      new()
      {
        Number = 93,
        Name = "Jazz",
        Chords = new List<string>
        {
          "A#M7/A",
          "G#6",
          "Am7",
          "A#M7",
          "Gadd11/B",
          "FM7/C",
          "A7/C",
          "D7/C",
          "D#M7b5/D",
          "Cadd9/D",
          "Fadd9",
          "Cdim7"
        }
      },
      new()
      {
        Number = 94,
        Name = "Jazz",
        Chords = new List<string>
        {
          "CM7",
          "DbM7",
          "Dm7",
          "Ebm7add13",
          "Em7b13",
          "Fm7",
          "F#m7b5",
          "G9sus",
          "G#dim7",
          "Am6",
          "F/Bb",
          "Bdim7"
        }
      },
      new()
      {
        Number = 95,
        Name = "Classical",
        Chords = new List<string>
        {
          "FM7/E",
          "Bdim/D",
          "Am",
          "G#dim7",
          "C/G",
          "Amb9/C",
          "Fadd9/C",
          "Dm",
          "C/G",
          "C/G",
          "Fadd9/G",
          "Ddim/G"
        }
      },
      new()
      {
        Number = 96,
        Name = "Classical",
        Chords = new List<string>
        {
          "Am",
          "Bm7/A",
          "Fdim/B",
          "C/E",
          "A7/G",
          "Dm7",
          "D#aug",
          "E",
          "F",
          "D/F#",
          "C/G",
          "E/G#"
        }
      },
      new()
      {
        Number = 97,
        Name = "Classical",
        Chords = new List<string>
        {
          "F#/C#",
          "F#/A#",
          "G#m7",
          "A6",
          "F#/C#",
          "Bsus2",
          "G#/C",
          "C#7",
          "A#7/D",
          "F#6sus4/C#",
          "C#m7",
          "C#7"
        }
      },
      new()
      {
        Number = 98,
        Name = "Classical",
        Chords = new List<string>
        {
          "Dm",
          "D#M7/D",
          "E7/D",
          "Dm7",
          "D7",
          "Gm/D",
          "G#/D",
          "A/D",
          "A#add9/D",
          "DM7(13)",
          "Csus2/D",
          "G#dim/D"
        }
      },
      new()
      {
        Number = 99,
        Name = "Modern",
        Chords = new List<string>
        {
          "CM13",
          "AM9/C#",
          "Dm13",
          "EbM13",
          "CM9/E",
          "FM13",
          "FmM13",
          "CM9(no 3)/G",
          "AbMb5#9",
          "Am11",
          "BbM9",
          "G6/9"
        }
      },
      new()
      {
        Number = 100,
        Name = "Modern",
        Chords = new List<string>
        {
          "CM9",
          "Cm9",
          "Dm9",
          "Dm6/9",
          "CM9/E",
          "Dm9/F",
          "D9/F#",
          "Gsus13",
          "G13b9",
          "Abmaj13",
          "AbDimM7",
          "BbM13"
        }
      }
    ];
  }

  private void SearchButton_Click(object sender, RoutedEventArgs e)
  {
    try
    {
      searchResults.Clear();

      // Collect non-empty search terms from TextBoxes
      List<string> searchTerms = [];
      foreach (var textBox in FindVisualChildren<TextBox>(this))
      {
        var trimmedText = textBox.Text.Trim();
        if (!string.IsNullOrWhiteSpace(trimmedText))
        {
          searchTerms.Add(trimmedText);
        }
      }

      if (searchTerms.Count != 0)
      {
        foreach (var chordSet in chordSets)
        {
          for (var k = 0; k < 12; k++) // Check all 12 transpositions
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

    }
    catch (Exception ex)
    {
      MessageBox.Show(ex.ToString());
    }
    // Search all chord sets and their transpositions
  }

  private static bool IsMatch(string chord, string term)
  {
    return new Glob(term).IsMatch(chord);
  }

  private string TransposeChord(string chord, int k)
  {
    var c = new Chord(chord);
    c.Transpose(k, "C#");
    return c.ChordName;
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
      for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
      {
        var child = VisualTreeHelper.GetChild(depObj, i);
        if (child is T tChild)
        {
          yield return tChild;
        }

        foreach (var childOfChild in FindVisualChildren<T>(child))
        {
          yield return childOfChild;
        }
      }
    }
  }
}