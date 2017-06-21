using System;

namespace SubscriptionApi
{
  public interface Log
  {
    void Error(Exception exception);
  }

  public class DummyLog : Log
  {
    public void Error(Exception exception)
    {
      Console.WriteLine(exception);
    }
  }
}