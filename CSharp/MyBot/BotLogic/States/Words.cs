using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace BotLogic.States
{
  public sealed class Words
  {
    public static Words From(ImmutableList<string> words)
    {
      return new Words(words);
    }

    private readonly ImmutableList<string> _words;

    private bool Equals(Words other)
    {
      return _words.SequenceEqual(other._words);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Words) obj);
    }

    public override int GetHashCode()
    {
      return (_words != null ? _words.GetHashCode() : 0);
    }


    private Words(ImmutableList<string> words)
    {
      _words = words;
    }

    public string AsSpaceSeparatedString()
    {
      return string.Join(" ", _words);
    }
  }
}