namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext;

public class FakeMain
{
  public FakeMain()
  {
    var postController = new PostController(new ResultInProgressFactory(),
      new CommandFactory(new ExistingPosts(), new Followers()));
  }
}