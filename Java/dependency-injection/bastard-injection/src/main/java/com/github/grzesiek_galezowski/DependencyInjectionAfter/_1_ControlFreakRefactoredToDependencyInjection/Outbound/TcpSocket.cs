using System;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Outbound
{
  public interface ISocket
  {
    void Open();
    void Close();
    void Send(string lol);
  }

  public class TcpSocket : ISocket
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