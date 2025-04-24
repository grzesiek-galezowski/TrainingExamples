using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Add;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Link;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion;

public class FakeMain
{
  public FakeMain()
  {
    var controller = new PostController(
      new AddPostHandler(new PostAssertions(), new ExistingPosts(), new Followers()),
      new LinkPostsHandler(new ExistingPosts(), new Followers()));
  }
}