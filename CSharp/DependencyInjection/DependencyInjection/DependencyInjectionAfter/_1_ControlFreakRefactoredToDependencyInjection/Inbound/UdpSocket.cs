using System;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Inbound
{
  internal interface IInboundSocket
  {
    bool Receive(out byte[] frameData);
  }

  class UdpSocket : IInboundSocket
  {
    public bool Receive(out byte[] frameData)
    {
      frameData = new byte[100];
      new Random().NextBytes(frameData);
      return true;
    }
  }
}