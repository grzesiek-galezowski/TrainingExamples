using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Add;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult;

public interface ICommandFactory
{
  IPostCommand CreateAddPostCommand(PostDto postDto, IAddingInProgress addingInProgress);
  IPostCommand CreateLinkPostsCommand(string id1, string id2, ILinkingInProgress linkingInProgress);
}