using ServiceLocatorDIAntipattern._2_ServiceLocator.InMessages;
using ServiceLocatorDIAntipattern._2_ServiceLocator.Interfaces;
using Unity;

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
        return ApplicationRoot.Context.Resolve<NullMessage>();
      }
      else if (frameData[0] == 1)
      {
        return ApplicationRoot.Context.Resolve<StartMessage>();
      }
      else
      {
        return ApplicationRoot.Context.Resolve<StopMessage>();
      }
      
    }
  }
}