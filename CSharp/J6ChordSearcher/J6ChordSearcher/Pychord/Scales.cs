using System.Collections.Generic;

namespace Pychord.Constants
{
  public static class Scales
  {
      /// <summary>
      /// Maps note names to their chromatic index values (0–11).
      /// </summary>
      public static readonly Dictionary<string, int> NoteValDict = new Dictionary<string, int>
        {
            { "Ab", 8 }, { "A", 9 }, { "A#", 10 }, { "Bb", 10 }, { "B", 11 }, { "Cb", 11 },
            { "C", 0 }, { "C#", 1 }, { "Db", 1 }, { "D", 2 }, { "D#", 3 }, { "Eb", 3 },
            { "E", 4 }, { "F", 5 }, { "F#", 6 }, { "Gb", 6 }, { "G", 7 }, { "G#", 8 }
        };

      /// <summary>
      /// Maps chromatic indices (0–11) to possible note names.
      /// </summary>
      public static readonly Dictionary<int, string[]> ValNoteDict = new Dictionary<int, string[]>
        {
            { 0, new[] { "C" } },
            { 1, new[] { "Db", "C#" } },
            { 2, new[] { "D" } },
            { 3, new[] { "Eb", "D#" } },
            { 4, new[] { "E" } },
            { 5, new[] { "F" } },
            { 6, new[] { "F#", "Gb" } },
            { 7, new[] { "G" } },
            { 8, new[] { "Ab", "G#" } },
            { 9, new[] { "A" } },
            { 10, new[] { "Bb", "A#" } },
            { 11, new[] { "B", "Cb" } }
        };

      /// <summary>
      /// Chromatic scale using sharps (0–11).
      /// </summary>
      public static readonly Dictionary<int, string> SharpedScale = new Dictionary<int, string>
        {
            { 0, "C" }, { 1, "C#" }, { 2, "D" }, { 3, "D#" }, { 4, "E" },
            { 5, "F" }, { 6, "F#" }, { 7, "G" }, { 8, "G#" }, { 9, "A" },
            { 10, "A#" }, { 11, "B" }
        };

      /// <summary>
      /// Chromatic scale using flats (0–11).
      /// </summary>
      public static readonly Dictionary<int, string> FlattedScale = new Dictionary<int, string>
        {
            { 0, "C" }, { 1, "Db" }, { 2, "D" }, { 3, "Eb" }, { 4, "E" },
            { 5, "F" }, { 6, "Gb" }, { 7, "G" }, { 8, "Ab" }, { 9, "A" },
            { 10, "Bb" }, { 11, "B" }
        };

      /// <summary>
      /// Maps scale roots to their preferred chromatic naming (sharps or flats).
      /// </summary>
      public static readonly Dictionary<string, Dictionary<int, string>> ScaleValDict = new Dictionary<string, Dictionary<int, string>>
        {
            { "Ab", FlattedScale }, { "A", SharpedScale }, { "A#", SharpedScale },
            { "Bb", FlattedScale }, { "B", SharpedScale }, { "Cb", FlattedScale },
            { "C", FlattedScale }, { "C#", SharpedScale }, { "Db", FlattedScale },
            { "D", SharpedScale }, { "D#", SharpedScale }, { "Eb", FlattedScale },
            { "E", SharpedScale }, { "F", FlattedScale }, { "F#", SharpedScale },
            { "Gb", FlattedScale }, { "G", SharpedScale }, { "G#", SharpedScale }
        };

      /// <summary>
      /// Maps modern musical modes to their relative semitone intervals.
      /// </summary>
      public static readonly Dictionary<string, int[]> RelativeKeyDict = new Dictionary<string, int[]>
        {
            { "maj", new[] { 0, 2, 4, 5, 7, 9, 11, 12 } }, // Ionian (Major)
            { "Dor", new[] { 0, 2, 3, 5, 7, 9, 10, 12 } }, // Dorian
            { "Phr", new[] { 0, 1, 3, 5, 7, 8, 10, 12 } },  // Phrygian
            { "Lyd", new[] { 0, 2, 4, 6, 7, 9, 11, 12 } },  // Lydian
            { "Mix", new[] { 0, 2, 4, 5, 7, 9, 10, 12 } },  // Mixolydian
            { "min", new[] { 0, 2, 3, 5, 7, 8, 10, 12 } },  // Aeolian (Minor)
            { "Loc", new[] { 0, 1, 3, 5, 6, 8, 10, 12 } }   // Locrian
        };
    }
}