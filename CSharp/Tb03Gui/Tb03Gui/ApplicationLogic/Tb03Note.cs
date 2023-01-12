using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using MidiPlayground;

namespace Tb03Gui.ApplicationLogic;

public record Tb03Note(
  string Name, 
  int BasePitch, 
  Tb03Octave Octave,
  bool Accent, 
  bool Slide, 
  int State)
{

  public int Pitch => BasePitch + 12 * (int)Octave;

  public override string ToString()
  {
    return $"{Name}{Environment.NewLine}({(int)Octave+1})";
  }

  private static readonly ImmutableDictionary<int, Tb03Note> NoteByBasePitch = ImmutableDictionary<int, Tb03Note>.Empty
    .Add(C0().BasePitch, C0())
    .Add(CSharp0().BasePitch, CSharp0())
    .Add(D0().BasePitch, D0())
    .Add(DSharp0().BasePitch, DSharp0())
    .Add(E0().BasePitch, E0())
    .Add(F0().BasePitch, F0())
    .Add(FSharp0().BasePitch, FSharp0())
    .Add(G0().BasePitch, G0())
    .Add(GSharp0().BasePitch, GSharp0())
    .Add(A0().BasePitch, A0())
    .Add(ASharp0().BasePitch, ASharp0())
    .Add(B0().BasePitch, B0());

  public static Tb03Note WithDefaultParams(string name, int basePitch, Tb03Octave octave)
  {
    return new Tb03Note(name, basePitch, octave, false, false, 1);
  }

  public static Tb03Note B0()
  {
    return WithDefaultParams("B", 11, 0);
  }

  public static Tb03Note ASharp0()
  {
    return WithDefaultParams("A#", 10, 0);
  }

  public static Tb03Note A0()
  {
    return WithDefaultParams("A", 9, 0);
  }

  public static Tb03Note GSharp0()
  {
    return WithDefaultParams("G#", 8, 0);
  }

  public static Tb03Note G0()
  {
    return WithDefaultParams("G", 7, 0);
  }

  public static Tb03Note FSharp0()
  {
    return WithDefaultParams("F#", 6, 0);
  }

  public static Tb03Note F0()
  {
    return WithDefaultParams("F", 5, 0);
  }

  public static Tb03Note E0()
  {
    return WithDefaultParams("E", 4, 0);
  }

  public static Tb03Note DSharp0()
  {
    return WithDefaultParams("D#", 3, 0);
  }

  public static Tb03Note D0()
  {
    return WithDefaultParams("D", 2, 0);
  }

  public static Tb03Note CSharp0()
  {
    return WithDefaultParams("C#", 1, 0);
  }

  public static Tb03Note C0()
  {
    return WithDefaultParams("C", 0, 0);
  }

  public Tb03Note TransposeTo(Tb03Octave octave)
  {
    return this with { Octave = octave };
  }

  public static Tb03Note From(int pitch, bool accent, bool slide, int state)
  {
    var basePitch = pitch % 12;
    var octave = pitch / 12;
    return NoteByBasePitch[basePitch] with
    {
      Octave = (Tb03Octave)octave,
      Accent = accent,
      Slide = slide,
      State = state
    };
  }

  public static IEnumerable<Tb03Note> NotesFrom(ImmutableArray<SequenceStepDto> steps)
  {
    return steps.Select(step => Tb03Note.From(step.Note, 
      Convert.ToBoolean(step.Accent), 
      Convert.ToBoolean(step.Slide), 
      step.State));
  }
}