using System;
using System.Collections;
using System.Collections.Generic;

namespace DealingWithNull
{
  public struct Maybe<T> : IEnumerable<T> where T : class
  {
    private readonly T _value;

    public Maybe(T value)
    {
      _value = value;
    }

    public T Value
    {
      get
      {
        if (!HasValue)
        {
          throw new InvalidOperationException();
        }
        return _value;
      }
    }

    public bool HasValue
    {
      get { return _value != null; }
    }


    public IEnumerator<T> GetEnumerator()
    {
      if (HasValue)
      {
        yield return _value;
      }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public static implicit operator Maybe<T>(T value)
    {
      return new Maybe<T>(value);
    }
  }
}