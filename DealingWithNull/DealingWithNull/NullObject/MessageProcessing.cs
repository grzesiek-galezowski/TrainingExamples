using System;

namespace DealingWithNull.NullObject
{
  public class MessageProcessing
  {
    private CommandFactory _factory;

    public MessageProcessing(CommandFactory factory)
    {
      _factory = factory;
    }

    public void PerformFor(Frame frame)
    {
      var command = _factory.CreateOneDescribedBy(frame);
    }
  }

  public class Problem
  {
    public void Main()
    {
      new MessageProcessing(new DefaultCommandFactory());
    }
  }

  public class DefaultCommandFactory : CommandFactory
  {
    public Command CreateOneDescribedBy(Frame frame)
    {
      throw new NotImplementedException();
    }
  }
}
