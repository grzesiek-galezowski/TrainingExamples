using System;

namespace DependencyInjectionBefore._1_ControlFreak.Outbound
{
  class TcpSocket
  {
    public void Open()
    {
      Console.WriteLine("open");
    }

    public void Close()
    {
      Console.WriteLine("closing");
    }

    public void Send(string lol)
    {
      Console.WriteLine(lol);
    }
  }
}