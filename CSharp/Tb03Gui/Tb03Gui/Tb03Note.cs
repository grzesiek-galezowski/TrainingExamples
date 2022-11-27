using System;

namespace Tb03Gui;

public record Tb03Note(string Name, int BasePitch, Tb03Octave Octave)
{
  public int Pitch => BasePitch + 12 * (int)Octave;
  public override string ToString()
  {
    return $"{Name}{Environment.NewLine}({(int)Octave+1})";
  }

  public static Tb03Note B0()
  {
    return new Tb03Note("B", 11, 0);
  }

  public static Tb03Note ASharp0()
  {
    return new Tb03Note("A#", 10, 0);
  }

  public static Tb03Note A0()
  {
    return new Tb03Note("A", 9, 0);
  }

  public static Tb03Note GSharp0()
  {
    return new Tb03Note("G#", 8, 0);
  }

  public static Tb03Note G0()
  {
    return new Tb03Note("G", 7, 0);
  }

  public static Tb03Note FSharp0()
  {
    return new Tb03Note("F#", 6, 0);
  }

  public static Tb03Note F0()
  {
    return new Tb03Note("F", 5, 0);
  }

  public static Tb03Note E0()
  {
    return new Tb03Note("E", 4, 0);
  }

  public static Tb03Note DSharp0()
  {
    return new Tb03Note("D#", 3, 0);
  }

  public static Tb03Note D0()
  {
    return new Tb03Note("D", 2, 0);
  }

  public static Tb03Note CSharp0()
  {
    return new Tb03Note("C#", 1, 0);
  }

  public static Tb03Note C0()
  {
    return new Tb03Note("C", 0, 0);
  }

  public Tb03Note TransposeTo(Tb03Octave octave)
  {
    return this with { Octave = octave };
  }
}