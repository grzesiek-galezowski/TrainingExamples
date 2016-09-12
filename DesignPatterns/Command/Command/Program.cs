using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Castle.DynamicProxy;
using Command.Commands;
using Command.Factories;
using Microsoft.Owin.Hosting;

namespace Command
{
  class Program
  {
    private static readonly ProxyGenerator Generator = new ProxyGenerator();

    static void Main(string[] args)
    {
      string baseAddress = "http://*:80/";

      using (WebApp.Start<Startup>(url: baseAddress))
      {
        var commandFactory = new DefaultGroupCommandFactory();
        new GroupsController(Synchronized(commandFactory), commandFactory);
        Thread.Sleep(Timeout.Infinite);
      }
    }

    public static GroupCommandFactory Synchronized(DefaultGroupCommandFactory defaultGroupCommandFactory)
    {
      return Generator.CreateInterfaceProxyWithTarget<GroupCommandFactory>(
        defaultGroupCommandFactory, new SynchronizingInterceptor());
    }
  }

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
      var result = _resultFactory.CreateResult<IEnumerable<string>>();
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
