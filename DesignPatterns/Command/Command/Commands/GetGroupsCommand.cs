using System.Collections.Generic;
using Command.DummyCode;
using Command.Factories;

namespace Command.Commands
{
  public class GetGroupsCommand : InboundCommand
  {
    private readonly AggregateResult<string> _result;
    private readonly Dep1 _d1;
    private readonly Dep4 _d4;

    public GetGroupsCommand(AggregateResult<string> result, Dep1 d1, Dep4 d4)
    {
      _result = result;
      _d1 = d1;
      _d4 = d4;
    }

    public void Execute()
    {
      _result.Value.Add("Grupa1");
      _result.Value.Add("Grupa2");
    }
  }
}