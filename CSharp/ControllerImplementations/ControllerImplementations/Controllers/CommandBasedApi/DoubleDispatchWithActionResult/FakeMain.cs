namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult;

public class FakeMain
{
  public FakeMain()
  {
    var postController = new PostController(new CommandFactory(new ExistingPosts(), new Followers()));
  }
}