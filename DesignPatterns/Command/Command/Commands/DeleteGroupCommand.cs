namespace Command.Commands
{
  public class DeleteGroupCommand : InboundCommand
  {
    private readonly int _id;

    public DeleteGroupCommand(int id)
    {
      _id = id;
    }

    public void Execute()
    {
      
    }
  }
}