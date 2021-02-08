using System;
using AutoFixture;
using NUnit.Framework;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Collections;
using TddXt.AnyRoot.Network;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;
using TddXt.AnyRoot.Time;
using static TddXt.AnyRoot.Root;

namespace XUnitTestPatterns._06_PickingTestValues
{
  public class _01_ConstrainedNonDeterminism
  {
    [Test]
    public void ShouldReturnItsInputWhenItIsNotNull()
    {
      //GIVEN
      var nonNullValue = Any.String();

      //WHEN
      var result = Transform(nonNullValue);

      //THEN
      Assert.AreEqual(nonNullValue + "a", result);
    }

    private string Transform(string value)
    {
      if (value == null)
      {
        return "FAIL";
      }
      else
      {
        return value + "a"; //TODO make it fail
      }
    }

    [Test]
    public void ShouldReturnItsInputWhenItIsNotNull2()
    {
      //GIVEN
      var fixture = new Fixture();
      var nonNullValue = fixture.Create<string>();

      //WHEN
      var result = Transform(nonNullValue);

      //THEN
      Assert.AreEqual(nonNullValue + "a", result);
    }

    [Test]
    public void PlainGenerators()
    {
      Any.Integer();
      Any.Instance<int>();

      Any.Instance<DateTime>();
      Any.DateTime();

      Any.Boolean();
      Any.Instance<bool>();

      Any.Char();
      Any.Instance<char>();

      //...etc

      Any.Uri();
      Any.UrlString();
      Any.LowerCaseAlphaChar();
      //...
    }

    [Test]
    public void Collections()
    {
      Any.Array<int>();
      Any.List<int>();
      Any.Dictionary<string, int>();
      Any.Dictionary<string, int>();
      //...
    }

    [Test]
    public void PlainConstrainedGenerators()
    {
      Any.OtherThan(1, 2, 4, 5);
      Any.StringContaining("abc");
      Any.String(123);
      //...
    }

    // GENERIC
    [Test]
    public void ShouldCreateGenericInstances()
    {
      //WHEN
      var anonymous = Any.Instance<MyClass<int>>();

      //THEN
      Assert.AreNotEqual(2, anonymous.Instance);
    }


    class MyClass<T>
    {
      public T Instance;

      public MyClass(T instance)
      {
        Instance = instance;
      }
    }

  }
}