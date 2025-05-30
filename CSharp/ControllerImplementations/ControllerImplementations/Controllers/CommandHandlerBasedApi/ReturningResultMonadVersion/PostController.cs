﻿using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Add;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Link;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion;
//variations:
//1. Returned result
//2. Exception filters / middleware

[Route("api/[controller]/posts")]
[ApiController]
public class PostController(AddPostHandler addPostHandler, LinkPostsHandler linkPostsHandler)
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
    var result = await addPostHandler.Handle(addPostCommand);

    return result.Match(Ok, IActionResult (error) => BadRequest(error.E));

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
    await linkPostsHandler.Handle(linkPostsCommand);

    return linkPostsCommand.Result
      .Match(Ok, IActionResult (error) => BadRequest(error.E));
  }
}