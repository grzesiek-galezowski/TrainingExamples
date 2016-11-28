using DependencyInjectionBefore._1_ControlFreak.InMessages;
using DependencyInjectionBefore._1_ControlFreak.Interfaces;

namespace DependencyInjectionBefore._1_ControlFreak.Inbound
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