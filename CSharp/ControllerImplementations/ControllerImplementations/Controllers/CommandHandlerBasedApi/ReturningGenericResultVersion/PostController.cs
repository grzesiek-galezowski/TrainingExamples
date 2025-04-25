using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Add;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Link;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;
//variations:
//1. Returned result
//2. Exception filters / middleware

[Route("api/[controller]/posts")]
[ApiController]
public class PostController(AddPostHandler<IActionResult> addPostHandler, LinkPostsHandler<IActionResult> linkPostsHandler)
  : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> AddPost([FromBody] PostDto post)
  {
    var addPostCommand = new AddPostCommand
    {
      Content = post.Content, //bug commands should not really contain DTOs
      Author = post.Author,
    };
    return await addPostHandler.Handle(addPostCommand);
  }

  [HttpPost]
  [Route("{id1}/link/{id2}")]
  public async Task<IActionResult> LinkPostsPost(string id1, string id2)
  {
    var linkPostsCommand = new LinkPostsCommand
    {
      Id1 = id1,
      Id2 = id2
    };
    return await linkPostsHandler.Handle(linkPostsCommand);
  }
}