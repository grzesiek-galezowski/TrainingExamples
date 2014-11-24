using System;

namespace DealingWithNull.Exceptions
{
  class HttpConnection : Connection
  {
    public void Send(string helloWorld)
    {
      Console.WriteLine("Sending " + helloWorld);
    }

    public bool Open()
    {
      return true;
    }
  }
}