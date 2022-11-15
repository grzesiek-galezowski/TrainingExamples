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