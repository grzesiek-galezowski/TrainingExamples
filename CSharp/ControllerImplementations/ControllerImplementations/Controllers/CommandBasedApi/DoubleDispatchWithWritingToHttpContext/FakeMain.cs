namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext;

public class FakeMain
{
  public FakeMain()
  {
    var postController = new PostController(new CommandFactory(new ExistingPosts(), new Followers()));
  }
}