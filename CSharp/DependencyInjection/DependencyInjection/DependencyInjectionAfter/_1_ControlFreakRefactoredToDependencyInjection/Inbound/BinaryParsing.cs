using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.InMessages;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Inbound
{
  internal interface IParsing
  {
    AcmeMessage ResultFor(byte[] frameData);
  }

  class BinaryParsing : IParsing
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