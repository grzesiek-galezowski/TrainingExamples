namespace Command.Commands
{
  public class GetGroupCommand : InboundCommand
  {
    private readonly Result<string> _result;

    public GetGroupCommand(Result<string> result)
    {
      _result = result;
    }

    public void Execute()
    {
      _result.Value = "Grupa1";
    }
  }
}