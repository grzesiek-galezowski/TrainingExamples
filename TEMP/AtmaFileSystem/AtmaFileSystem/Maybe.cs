using System;

namespace AtmaFileSystem
{
  public static class Maybe
  {
    public static Maybe<T> Wrap<T>(T instance) where T : class
    {
      return new Maybe<T>(instance);
    }
  }

  public struct Maybe<T> where T : class
  {
    private readonly T _value;
    private static readonly Maybe<T> _not = new Maybe<T>();

    public Maybe(T instance)
      : this()
    {
      if (instance != null)
      {
        Found = true;
        _value = instance;
      }
    }

    public bool Found { get; private set; }

    public T Value()
    {
      if (Found)
      {
        return _value;
      }
      else
      {
        throw new InvalidOperationException("No instance of type " + typeof (T));
      }
    }

    public static implicit operator Maybe<T> (T instance)
    {
      return Maybe.Wrap(instance);
    }

    public static Maybe<T> Not
    {
      get { return _not; }
    }

    public T ValueOr(T alternativeValue)
    {
      return Found ? Value() : alternativeValue;
    }

    public Maybe<T> Otherwise(Maybe<T> alternative)
    {
      return Found ? this : alternative;
    }


    public override string ToString()
    {
      return Found ? Value().ToString() : "<Nothing>";
    }

    public Maybe<U> To<U>() where U : class
    {
      if (!Found)
      {
        return Maybe<U>.Not;
      }
      else
      {
        return Maybe.Wrap(Value() as U);
      }
    }
  }
}