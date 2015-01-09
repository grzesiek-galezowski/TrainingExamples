using System;

namespace DependencyInjectionBefore._1_ControlFreak.Inbound
{
  class UdpSocket
  {
    public bool Receive(out byte[] frameData)
    {
      frameData = new byte[100];
      new Random().NextBytes(frameData);
      return true;
    }
  }
}