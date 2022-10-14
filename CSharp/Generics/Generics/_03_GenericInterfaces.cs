using System.Reflection;
using FluentAssertions;

namespace Generics;

public class _03_GenericInterfaces
{
  private interface IConfigSectionObserver<in T>
  {
    public void NotifyOf(T value);
  }

  private class MultipleSectionsObserver
    : IConfigSectionObserver<ConnectionSection>,
      IConfigSectionObserver<LoggingSection>
  {
    public void NotifyOf(ConnectionSection value)
    {
      throw new NotImplementedException(nameof(ConnectionSection));
    }

    public void NotifyOf(LoggingSection value)
    {
      throw new NotImplementedException(nameof(LoggingSection));
    }
  }

  [Test]
  public void Test1()
  {
    var observer = new MultipleSectionsObserver();
    IConfigSectionObserver<ConnectionSection> connObserver = observer;
    IConfigSectionObserver<LoggingSection> loggingObserver = observer;

    connObserver.Invoking(o => o.NotifyOf(new ConnectionSection()))
      .Should().ThrowExactly<NotImplementedException>()
      .WithMessage(nameof(ConnectionSection));
    
    loggingObserver.Invoking(o => o.NotifyOf(new LoggingSection()))
      .Should().ThrowExactly<NotImplementedException>()
      .WithMessage(nameof(LoggingSection));
  }

  private class LoggingSection
  {
  }

  private class ConnectionSection
  {
  }
}

public class _04_GenericMethods
{
  class Serializer
  {
    //generic methods typically mean reflection or downcasting
    //- see the Select method implementation below for imperfect downcasting example
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