using System.Reflection;
using FluentAssertions;

namespace Generics;

public class _04_GenericMethods
{
  class Serializer
  {
    //generic methods typically mean reflection or downcasting
    public static string Serialize<T>(T obj)
    {
      string str = "";

      return string.Join('|',
        typeof(T).GetFields().Select(FieldAsString(obj)));
    }

    private static Func<FieldInfo, string> FieldAsString<T>(T obj)
    {
      return fi => fi.Name + ": " + fi.GetValue(obj);
    }

    public class Data
    {
      public Data(int a, string b)
      {
        this.a = a;
        this.b = b;
      }

      public int a;
      public string b;
    }

    [Test]
    public void ShouldXXXXXXXXXXXXX() //bug
    {
      //GIVEN
      var data = new Data(1, "abc");

      //WHEN
      var str = Serializer.Serialize(data);

      //THEN
      str.Should().Be("a: 1|b: abc");
    }
  }
}