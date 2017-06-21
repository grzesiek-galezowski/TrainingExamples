using TelecomSystemNestedFunctions.InMessages;
using TelecomSystemNestedFunctions.Interfaces;

namespace TelecomSystemNestedFunctions.Inbound
{
  public interface IParsing
  {
    AcmeMessage ResultFor(byte[] frameData);
  }

  public class BinaryParsing : IParsing
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