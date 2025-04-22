using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Add;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult;

public class CommandFactory(IExistingPosts existingPosts, IFollowers followers) : ICommandFactory
{
  public IPostCommand CreateAddPostCommand(PostDto postDto, IAddingInProgress addingInProgress)
  {
    return new CreatePostCommand(
      new AddPostRequest(postDto), 
      existingPosts,
      followers, 
      addingInProgress);
  }

  public IPostCommand CreateLinkPostsCommand(string id1, string id2, ILinkingInProgress linkingInProgress)
  {
    return new LinkPostsCommand(id1, id2, existingPosts, followers, linkingInProgress);
  }
}