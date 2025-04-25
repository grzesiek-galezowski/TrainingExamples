using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Add;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Link;
using Microsoft.AspNetCore.Mvc;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

public class FakeMain
{
  public FakeMain()
  {
    var controller = new PostController(
      new AddPostHandler<IActionResult>(new PostAssertions(), new ExistingPosts(), new Followers(), new ActionResultResponseFactory()),
      new LinkPostsHandler<IActionResult>(new ExistingPosts(), new Followers(), new ActionResultResponseFactory()));
  }
}