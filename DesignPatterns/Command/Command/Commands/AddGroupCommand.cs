namespace Command.Commands
{
  public class AddGroupCommand : InboundCommand
  {
    private readonly string _value;

    public AddGroupCommand(string value)
    {
      _value = value;
    }

    public void Execute()
    {
    }
  }
}