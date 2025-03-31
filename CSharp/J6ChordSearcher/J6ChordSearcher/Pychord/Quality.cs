﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Pychord
{
  public class Quality
  {
    // Private fields
    private readonly string _quality; // Name of the quality (e.g., "m7", "sus4")
    private int[] _components;        // Components as semitone intervals from root

    /// <summary>
    /// Constructor for Quality instance.
    /// </summary>
    /// <param name="name">Name of the quality.</param>
    /// <param name="components">Components of the quality as semitone intervals.</param>
    public Quality(string name, int[] components)
    {
      _quality = name ?? throw new ArgumentNullException(nameof(name));
      _components = components?.ToArray() ?? throw new ArgumentNullException(nameof(components));
    }

    // Property for quality name (equivalent to Python @property)
    public string QualityString => _quality;

    // Override ToString (equivalent to Python __str__ and __unicode__)
    public override string ToString() => _quality;

    // Equality comparison (equivalent to Python __eq__)
    public override bool Equals(object obj)
    {
      if (!(obj is Quality other))
        throw new ArgumentException($"Cannot compare Quality with {obj?.GetType().Name ?? "null"}");
      return _components.SequenceEqual(other._components);
    }

    public override int GetHashCode() => _components.Aggregate(_quality.GetHashCode(), (hash, c) => hash ^ c);

    public static bool operator ==(Quality left, Quality right) => left?.Equals(right) ?? right is null;
    public static bool operator !=(Quality left, Quality right) => !(left == right);

    /// <summary>
    /// Gets the components of the chord quality relative to a root note.
    /// </summary>
    /// <param name="root">The root note of the chord (default "C").</param>
    /// <param name="visible">If true, returns note names; otherwise, returns semitone values.</param>
    /// <returns>List of components (note names or integers).</returns>
    public List<int> GetComponents(string root = "C")
    {
      int rootVal = Utils.NoteToVal(root);
      var components = _components.Select(v => v + rootVal).ToList();
      return components;
    }

    /// <summary>
    /// Gets the components of the chord quality relative to a root note.
    /// </summary>
    /// <param name="root">The root note of the chord (default "C").</param>
    /// <param name="visible">If true, returns note names; otherwise, returns semitone values.</param>
    /// <returns>List of components (note names or integers).</returns>
    public List<object> GetComponentsVisible(string root = "C", bool visible = false)
    {
      int rootVal = Utils.NoteToVal(root);
      var components = _components.Select(v => v + rootVal).ToList();
      if (visible)
      {
        return components.Select(c => Utils.ValToNote(c, root)).ToList<object>();
      }
      else
      {
        return components.Cast<object>().ToList();
      }
    }

    /// <summary>
    /// Appends a bass note (slash chord) to the quality's components.
    /// </summary>
    /// <param name="onChord">The bass note (e.g., "G" for Am7/G).</param>
    /// <param name="root">The root note of the chord (e.g., "A" for Am7/G).</param>
    public void AppendOnChord(string onChord, string root)
    {
      int rootVal = Utils.NoteToVal(root);
      int onChordVal = Utils.NoteToVal(onChord) - rootVal;

      var components = _components.ToList();
      for (int i = 0; i < components.Count; i++)
      {
        if (components[i] % 12 == onChordVal)
        {
          components.RemoveAt(i);
          break;
        }
      }

      if (onChordVal > rootVal)
        onChordVal -= 12;

      if (!components.Contains(onChordVal))
      {
        components = new List<int> { onChordVal }
            .Concat(components.Where(v => v % 12 != onChordVal % 12))
            .ToList();
      }

      _components = components.ToArray();
    }
  }

  public class QualityManager
  {
    // Singleton instance
    private static readonly Lazy<QualityManager> _instance = new Lazy<QualityManager>(() => new QualityManager(), true);
    public static QualityManager Instance => _instance.Value;

    // Private dictionary of qualities
    private Dictionary<string, Quality> _qualities;

    // Private constructor for singleton
    public QualityManager()
    {
      LoadDefaultQualities();
    }

    /// <summary>
    /// Loads the default qualities from Constants.Qualities.DefaultQualities.
    /// </summary>
    private void LoadDefaultQualities()
    {
      _qualities = Constants.Qualities.DefaultQualities
          .ToDictionary(kvp => kvp.Key, kvp => new Quality(kvp.Key, kvp.Value));
    }

    /// <summary>
    /// Gets a quality by name, optionally inverted.
    /// </summary>
    /// <param name="name">Name of the quality.</param>
    /// <param name="inversion">Number of inversions to apply.</param>
    /// <returns>A new Quality instance.</returns>
    public Quality GetQuality(string name, int inversion = 0)
    {
      if (!_qualities.ContainsKey(name))
        throw new ArgumentException($"Unknown quality: {name}");

      // Create a deep copy of the quality
      var quality = new Quality(_qualities[name].QualityString, _qualities[name].GetComponents().Select(o => (int)o).ToArray());

      // Apply inversions
      for (int i = 0; i < inversion; i++)
      {
        int firstNote = quality.GetComponents().Cast<int>().First();
        int lastNote = quality.GetComponents().Cast<int>().Last();
        int n = firstNote;
        while (n < lastNote)
          n += 12;
        var newComponents = quality.GetComponents().Skip(1).Select(o => (int)o).Append(n).ToArray();
        quality = new Quality(quality.QualityString, newComponents);
      }

      return quality;
    }

    /// <summary>
    /// Gets all qualities as a dictionary.
    /// </summary>
    public Dictionary<string, Quality> GetQualities()
    {
      return new Dictionary<string, Quality>(_qualities);
    }

    /// <summary>
    /// Sets a new quality or updates an existing one.
    /// </summary>
    /// <param name="name">Name of the quality.</param>
    /// <param name="components">Components of the quality as semitone intervals.</param>
    public void SetQuality(string name, int[] components)
    {
      _qualities[name] = new Quality(name, components);
    }

    /// <summary>
    /// Finds a quality matching the given components.
    /// </summary>
    /// <param name="components">Components to match.</param>
    /// <returns>A new Quality instance if found; otherwise, null.</returns>
    public Quality FindQualityFromComponents(int[] components)
    {
      foreach (var q in _qualities.Values)
      {
        if (q.GetComponents().Cast<int>().SequenceEqual(components))
          return new Quality(q.QualityString, q.GetComponents().Cast<int>().ToArray());
      }
      return null;
    }
  }
}