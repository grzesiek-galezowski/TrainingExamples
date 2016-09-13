using System.Collections.Generic;
using Command.Commands;
using Command.Results;

namespace Command.Factories
{
  public class DefaultGroupCommandFactory : GroupCommandFactory, ResultFactory
  {
    public InboundCommand CreateGetGroupsCommand(AggregateResult<string> result)
    {
      return new GetGroupsCommand(result);
    }

    public InboundCommand CreateGetGroupCommand(int id, Result<string> result)
    {
      return new GetGroupCommand(result);
    }

    public InboundCommand CreateAddGroupCommand(string value)
    {
      return new AddGroupCommand(value);
    }

    public InboundCommand CreateModifyGroupCommand(int id, string value)
    {
      return new AggregateCommand(
        new LogAndSwallowExceptionsCommand(new DeleteGroupCommand(id)),
        new LogAndSwallowExceptionsCommand(new AddGroupCommand(value)));
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