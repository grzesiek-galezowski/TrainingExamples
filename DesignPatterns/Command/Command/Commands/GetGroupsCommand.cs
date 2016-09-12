using System.Collections.Generic;

namespace Command.Commands
{
  public class GetGroupsCommand : InboundCommand
  {
    private readonly Result<IEnumerable<string>> _result;

    public GetGroupsCommand(Result<IEnumerable<string>> result)
    {
      _result = result;
    }

    public void Execute()
    {
      _result.Value = new List<string>() { "Grupa1", "Grupa2" };
    }
  }
}