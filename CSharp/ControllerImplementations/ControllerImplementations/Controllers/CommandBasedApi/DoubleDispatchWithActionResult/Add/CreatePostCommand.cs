using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;
using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Add;

internal class CreatePostCommand(
  IAddPostRequest requestedPost,
  IExistingPosts existingPosts,
  IFollowers followers,
  IAddingInProgress addingInProgress)
  : IPostCommand
{
  public async Task Execute()
  {
    try
    {
      requestedPost.AssertContentIsOfRequiredLength();
      requestedPost.AssertContentContainsNoInappropriateWords();
      await requestedPost.AddTo(existingPosts, addingInProgress);
      await requestedPost.Notify(followers, addingInProgress);
    }
    catch (Exception e)
    {
      addingInProgress.FailedBecauseOf(e);
    }
  }
}