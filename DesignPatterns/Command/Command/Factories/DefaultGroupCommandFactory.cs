using System.Collections.Generic;
using Command.Commands;
using Command.DummyCode;
using Command.Results;

namespace Command.Factories
{
  public class DefaultGroupCommandFactory : GroupCommandFactory, ResultFactory
  {
    private readonly Dep1 _d1;
    private readonly Dep2 _d2;
    private readonly Dep3 _d3;
    private readonly Dep4 _d4;
    private readonly Dep5 _d5;

    public DefaultGroupCommandFactory(Dep1 d1, Dep2 d2, Dep3 d3, Dep4 d4, Dep5 d5)
    {
      _d1 = d1;
      _d2 = d2;
      _d3 = d3;
      _d4 = d4;
      _d5 = d5;
    }

    public InboundCommand CreateGetGroupsCommand(AggregateResult<string> result)
    {
      return new GetGroupsCommand(result, _d1, _d4);
    }

    public InboundCommand CreateGetGroupCommand(int id, Result<string> result)
    {
      return new GetGroupCommand(result, _d2, _d4);
    }

    public InboundCommand CreateAddGroupCommand(string value)
    {
      return new AddGroupCommand(value, _d3, _d5);
    }

    public InboundCommand CreateModifyGroupCommand(int id, string value)
    {
      return new AggregateCommand(
        new LogAndSwallowExceptionsCommand(CreateDeleteGroupCommand(id)),
        new LogAndSwallowExceptionsCommand(CreateAddGroupCommand(value)));
    }

    public InboundCommand CreateDeleteGroupCommand(int id)
    {
      return new LogAndSwallowExceptionsCommand(new DeleteGroupCommand(id));
    }

    public Result<T> CreateResult<T>()
    {
      return new ConcreteResult<T>();
    }

    public AggregateResult<T> CreateAggregateResult<T>()
    {
      return new ConcreteAggregateResult<T>();
    }
  }


}