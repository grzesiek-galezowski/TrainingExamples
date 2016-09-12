using System.Collections.Generic;

namespace Command
{
  public interface GroupCommandFactory
  {
    InboundCommand CreateGetGroupsCommand(AggregateResult<string> result);
    InboundCommand CreateGetGroupCommand(int id, Result<string> result);
    InboundCommand CreateAddGroupCommand(string value);
    InboundCommand CreateModifyGroupCommand(int id, string value);
    InboundCommand CreateDeleteGroupCommand(int id);
  }


  public interface InboundCommand
  {
    void Execute();
  }

  public interface ResultFactory
  {
    Result<T> CreateResult<T>();
    AggregateResult<T> CreateAggregateResult<T>();
  }

  public interface AggregateResult<T>
  {
    IList<T> Value { get; }
  }

  public interface Result<T>
  {
    T Value { get; set; }
  }
}