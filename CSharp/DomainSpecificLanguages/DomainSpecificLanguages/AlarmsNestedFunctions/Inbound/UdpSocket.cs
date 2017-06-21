using System;

namespace TelecomSystemNestedFunctions.Inbound
{
  public interface IInboundSocket
  {
    bool Receive(out byte[] frameData);
  }

  public class UdpSocket : IInboundSocket
  {
    public bool Receive(out byte[] frameData)
    {
      frameData = new byte[100];
      new Random().NextBytes(frameData);
      return true;
    }
  }
}