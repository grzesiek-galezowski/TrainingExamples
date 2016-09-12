using System.Collections.Generic;

namespace Command
{
  public interface GroupCommandFactory
  {
    InboundCommand CreateGetGroupsCommand(Result<IEnumerable<string>> result);
    InboundCommand CreateGetGroupCommand(int id, Result<string> result);
    InboundCommand CreateAddGroupCommand(string value);
    InboundCommand CreateModifyGroupCommand(int id, string value);
    InboundCommand CreateDeleteGroupCommand(int id);
  }

  public interface ResultFactory
  {
    Result<T> CreateResult<T>();
  }

  public interface InboundCommand
  {
    void Execute();
  }

  public interface Result<T>
  {
    T Value { get; set; }
  }
}