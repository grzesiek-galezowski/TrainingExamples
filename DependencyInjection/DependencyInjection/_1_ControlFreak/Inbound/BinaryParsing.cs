using DependencyInjection._1_ControlFreak.InMessages;
using DependencyInjection._1_ControlFreak.Interfaces;

namespace DependencyInjection._1_ControlFreak.Inbound
{
  class BinaryParsing
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