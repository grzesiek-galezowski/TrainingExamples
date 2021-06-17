using BastardInjection._4_BastardInjection.InMessages;
using BastardInjection._4_BastardInjection.Interfaces;

namespace BastardInjection._4_BastardInjection.Inbound
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