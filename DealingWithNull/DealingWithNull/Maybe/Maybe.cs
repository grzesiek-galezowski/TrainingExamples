using System;

namespace DealingWithNull.Maybe
{
  public struct Maybe<T>
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

    public static implicit operator Maybe<T>(T value)
    {
      return new Maybe<T>(value);
    }

  }

  public static class MaybeExtensions
  {
    public static Maybe<TB> Bind<T, TB>(this Maybe<T> maybe, Func<T, Maybe<TB>> func)
    {
      return maybe.HasValue ?
        func(maybe.Value) : default(Maybe<TB>);
    }


    public static Maybe<TC> SelectMany<TA, TB, TC>(this Maybe<TA> a, Func<TA, Maybe<TB>> func, Func<TA, TB, TC> selectFunction)
    {
      return a.Bind(aval => func(aval).Bind<TB, TC>(bval => selectFunction(aval, bval)));
    }

    public static Maybe<TB> Select<TA, TB>(this Maybe<TA> a, Func<TA, TB> select)
    {
      return a.Bind<TA, TB>(aval => select(aval));
    }
  }
}