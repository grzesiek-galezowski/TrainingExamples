using System.Collections.Generic;
using System.Web.Http;

namespace Command
{
  public class GroupsController : ApiController
  {
    private readonly GroupCommandFactory _commandFactory;
    private readonly ResultFactory _resultFactory;

    public GroupsController(GroupCommandFactory commandFactory, ResultFactory resultFactory)
    {
      _commandFactory = commandFactory;
      _resultFactory = resultFactory;
    }
    // GET api/values 
    public IEnumerable<string> Get()
    {
      var result = _resultFactory.CreateAggregateResult<string>();
      var getUsersCommand = _commandFactory.CreateGetGroupsCommand(result);
      getUsersCommand.Execute();
      return result.Value;
    }

    // GET api/values/5 
    public string Get(int id)
    {
      var result = _resultFactory.CreateResult<string>();
      var getUsersCommand = _commandFactory.CreateGetGroupCommand(id, result);
      getUsersCommand.Execute();
      return result.Value;
    }

    // POST api/values 
    public void Post([FromBody] string value)
    {
      var postUserCommand = _commandFactory.CreateAddGroupCommand(value);
      postUserCommand.Execute();
    }

    // PUT api/values/5 
    public void Put(int id, [FromBody] string value)
    {
      var putUserCommand = _commandFactory.CreateModifyGroupCommand(id, value);
      putUserCommand.Execute();
    }

    // DELETE api/values/5 
    public void Delete(int id)
    {
      var putUserCommand = _commandFactory.CreateDeleteGroupCommand(id);
      putUserCommand.Execute();
    }
  }
}