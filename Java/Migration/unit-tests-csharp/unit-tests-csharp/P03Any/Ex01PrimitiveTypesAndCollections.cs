using System;
using NUnit.Framework;
using TddEbook.TddToolkit;

namespace unit_tests_csharp.P03Any
{
  public class Ex01PrimitiveTypesAndCollections
  {

    [Test]
    public void ShouldReturnItsInputWhenItIsNotNull()
    {
      //GIVEN
      var nonNullValue = Any.String();

      //WHEN
      var result = Transform(nonNullValue);

      // THEN
      Assert.AreEqual(nonNullValue, result);
    }

    private string Transform(string value)
    {
      if(value == null)
      {
        return "FAIL";
      }
      else
      {
        return value; //TODO make it fail
      }
    }

    [Test]
    public void PlainGenerators()
    {
      Any.Integer();
      Any.Instance<int>();

      Any.DateTime();
      Any.Instance<DateTime>();

      Any.Boolean();
      Any.Instance<bool>();

      Any.Char();
      Any.Instance<char>();

      //...etc

      Any.Uri();
      Any.IpAddress();
      //...
    }

    [Test]
    public void Collections()
    {
      Any.Array<int>();
      Any.List<int>();
      Any.Dictionary<string, int>();
      //...
    }

    [Test]
    public void PlainConstrainedGenerators()
    {
      Any.IntegerOtherThan(1,2,4,5);
      Any.StringContaining("abc");
      Any.StringOfLength(123);
      //...
    }

    [Test]
    public void ConstrainedGenerators()
    {
      var anInt = Any.OtherThan(123);
      var stringsWithoutAbc = Any.ListWithout("abc");
      var integers = Any.ArrayWithout(123);
    }
  }
}
