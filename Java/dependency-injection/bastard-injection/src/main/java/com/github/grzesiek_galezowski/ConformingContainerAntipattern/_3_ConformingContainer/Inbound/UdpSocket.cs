using System;

namespace ConformingContainerAntipattern._3_ConformingContainer.Inbound
{
  internal interface IInputSocket
  {
    bool Receive(out byte[] frameData);
  }

  class UdpSocket : IInputSocket
  {
    public bool Receive(out byte[] frameData)
    {
      frameData = new byte[100];
      ApplicationRoot.Context.Resolve<Random>().NextBytes(frameData); //stable dependency!
      return true;
    }
  }
}