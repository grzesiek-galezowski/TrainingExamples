using Command.DummyCode;
using Command.Factories;

namespace Command.Commands
{
  public class AddGroupCommand : InboundCommand
  {
    private readonly string _value;
    private readonly Dep3 _d3;
    private readonly Dep5 _d5;

    public AddGroupCommand(string value, Dep3 d3, Dep5 d5)
    {
      _value = value;
      _d3 = d3;
      _d5 = d5;
    }

    public void Execute()
    {
    }
  }
}