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

  public static class MaybeExtensions
  {
    public static Maybe<B> Bind<T, B>(this Maybe<T> maybe, Func<T, Maybe<B>> func)
      where B : class
      where T : class
    {
      return maybe.HasValue ?
        func(maybe.Value) : default(Maybe<B>);
    }


    public static Maybe<TC> SelectMany<TA, TB, TC>(this Maybe<TA> a, Func<TA, Maybe<TB>> func, Func<TA, TB, TC> selectFunction)
      where TA : class
      where TB : class
      where TC : class
    {
      return a.Bind(aval => func(aval).Bind<TA, TB>(bval => selectFunction(aval, bval)));
    }
  }
}