using System;

namespace BastardInjection._4_BastardInjection.Inbound
{
  internal interface IInputSocket : IDisposable //first error - do not implement disposabled in interfaces
  {
    bool Receive(out byte[] frameData);
  }

  class UdpSocket : IInputSocket
  {
    public bool Receive(out byte[] frameData)
    {
      frameData = new byte[100];
      new Random().NextBytes(frameData);
      return true;
    }

    public void Dispose()
    {
      Console.WriteLine("Disposed");
    }
  }
}