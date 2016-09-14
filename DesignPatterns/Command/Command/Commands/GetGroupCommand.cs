using Command.DummyCode;
using Command.Factories;

namespace Command.Commands
{
  public class GetGroupCommand : InboundCommand
  {
    private readonly Result<string> _result;
    private readonly Dep2 _d2;
    private readonly Dep4 _d4;

    public GetGroupCommand(Result<string> result, Dep2 d2, Dep4 d4)
    {
      _result = result;
      _d2 = d2;
      _d4 = d4;
    }

    public void Execute()
    {
      _result.Value = "Grupa1";
    }
  }
}