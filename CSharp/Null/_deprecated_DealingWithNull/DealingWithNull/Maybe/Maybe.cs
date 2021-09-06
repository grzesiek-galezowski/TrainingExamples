using System;

namespace DealingWithNull.Maybe
{
  public static class Maybe
  {
    public static Maybe<T> Wrap<T>(T instance)
    {
      return new Maybe<T>(instance);
    }
  }

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

    public T ValueOr(T alternativeValue)
    {
      return HasValue ? Value : alternativeValue;
    }

    public override string ToString()
    {
      return HasValue ? Value.ToString() : "<Nothing>";
    }
  }

  public static class MaybeExtensions
  {
    public static Maybe<TB> Bind<T, TB>(this Maybe<T> maybe, Func<T, Maybe<TB>> func)
    {
      if (maybe.HasValue)
      {
        return func(maybe.Value);
      }
      else
      {
        return default(Maybe<TB>);
      }
    }

    public static Maybe<TFResult> SelectMany<TSource, TB, TFResult>(this Maybe<TSource> a, Func<TSource, Maybe<TB>> func, Func<TSource, TB, TFResult> selectFunction)
    {
      return a.Bind(aval => 
      {
        var result = func(aval);
        return result.Bind<TB, TFResult>(bval => selectFunction(aval, bval));
      });
    }

    public static Maybe<TB> Select<TA, TB>(this Maybe<TA> a, Func<TA, TB> select)
    {
      return a.Bind<TA, TB>(
        aval => select(aval)
      );
    }
  }
}