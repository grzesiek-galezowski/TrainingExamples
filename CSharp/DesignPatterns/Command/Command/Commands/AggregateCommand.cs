namespace Command.Commands
{
  public class AggregateCommand : InboundCommand
  {
    private readonly InboundCommand[] _commands;

    public AggregateCommand(params InboundCommand[] commands)
    {
      _commands = commands;
    }

    public void Execute()
    {
      foreach (var command in _commands)
      {
        command.Execute();
      }
    }
  }
}