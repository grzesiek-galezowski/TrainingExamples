using ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Add;
using ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Link;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public class FakeMain
{
  public FakeMain()
  {
    var controller = new PostController(
      new AddPostHandler(new PostAssertions(), new ExistingPosts(), new Followers()),
      new LinkPostsHandler(new ExistingPosts(), new Followers()));
  }
}