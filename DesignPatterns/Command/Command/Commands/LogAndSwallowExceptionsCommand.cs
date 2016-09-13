using System;

namespace Command.Commands
{
  public class LogAndSwallowExceptionsCommand : InboundCommand
  {
    private readonly InboundCommand _command;

    public LogAndSwallowExceptionsCommand(InboundCommand command)
    {
      _command = command;
    }

    public void Execute()
    {
      try
      {
        _command.Execute();
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
      }
    }
  }
}