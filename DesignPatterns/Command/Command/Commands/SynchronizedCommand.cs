using System;

namespace Command.Commands
{
  public class SynchronizedCommand : InboundCommand
  {
    private readonly InboundCommand _command;
    private readonly object _syncRoot;

    public SynchronizedCommand(InboundCommand command, object syncRoot)
    {
      _command = command;
      _syncRoot = syncRoot;
    }

    public void Execute()
    {
      lock (_syncRoot)
      {
        _command.Execute();
      }
    }
  }
}