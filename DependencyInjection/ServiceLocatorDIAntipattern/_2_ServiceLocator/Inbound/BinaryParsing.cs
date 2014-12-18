using ServiceLocatorDIAntipattern._2_ServiceLocator.InMessages;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Interfaces;

namespace ServiceLocatorDIAntipattern._2_ServiceLocator.Inbound
{
  internal interface IPacketParsing
  {
    AcmeMessage ResultFor(byte[] frameData);
  }

  class BinaryParsing : IPacketParsing
  {
    public AcmeMessage ResultFor(byte[] frameData)
    {
      if (frameData == null)
      {
        return new NullMessage();
      }
      else if (frameData[0] == 1)
      {
        return new StartMessage();
      }
      else
      {
        return new StopMessage();
      }
      
    }
  }
}