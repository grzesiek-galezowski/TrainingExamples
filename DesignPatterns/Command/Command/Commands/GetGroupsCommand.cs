using System.Collections.Generic;

namespace Command.Commands
{
  public class GetGroupsCommand : InboundCommand
  {
    private readonly AggregateResult<string> _result;

    public GetGroupsCommand(AggregateResult<string> result)
    {
      _result = result;
    }

    public void Execute()
    {
      _result.Value.Add("Grupa1");
      _result.Value.Add("Grupa2");
    }
  }
}