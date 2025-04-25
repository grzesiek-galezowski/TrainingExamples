using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Add;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Link;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion;

public class FakeMain
{
  public FakeMain()
  {
    var controller = new PostController(
      new AddPostHandler(new PostAssertions(), new ExistingPosts(), new Followers()),
      new LinkPostsHandler(new ExistingPosts(), new Followers()));
  }
}