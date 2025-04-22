using System.Threading;
using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Add;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Link;
using Microsoft.AspNetCore.Mvc;

//only commands, no queries
//0. Facade
//1. Command based API
//2. Handler based API
//3. MediatR
//4. #nocontroller


//0 decorated command
//1 result in progress as field
//2 result in progress as part of command
//2 result in progress as bool
//3 result in progress as "this"
//4 boolean result for command and command<T>
//5 Synchronous/asynchronous command factory
//6 dispatcher
namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext;

[Route("api/[controller]/posts")]
[ApiController]
public class PostController(ICommandFactory commandFactory)
  : ControllerBase
{
  [HttpPost]
  public async Task AddPost([FromBody] PostDto post, CancellationToken token)
  {
    await commandFactory.CreateAddPostCommand(post, new AddingInProgress(this)).Execute(token);
  }

  [HttpPost]
  [Route("{id1}/link/{id2}")]
  public async Task LinkPostsPost(string id1, string id2, CancellationToken token)
  {
    await commandFactory.CreateLinkPostsCommand(id1, id2, new LinkingInProgress(this)).Execute(token);
  }

}