using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Add;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;
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
namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult;

[Route("api/[controller]/posts")]
[ApiController]
public class PostController(ICommandFactory commandFactory)
  : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> AddPost([FromBody] PostDto post)
  {
    var addingInProgress = new AddingInProgress();
    await commandFactory.CreateAddPostCommand(post, addingInProgress).ExecuteAsync();
    return addingInProgress.Result();
  }

  [HttpPost]
  [Route("{id1}/link/{id2}")]
  public async Task<IActionResult> LinkPostsPost(string id1, string id2)
  {
    var addingInProgress = new LinkingInProgress();
    await commandFactory.CreateLinkPostsCommand(id1, id2, addingInProgress).ExecuteAsync();
    return addingInProgress.Result();
  }

}