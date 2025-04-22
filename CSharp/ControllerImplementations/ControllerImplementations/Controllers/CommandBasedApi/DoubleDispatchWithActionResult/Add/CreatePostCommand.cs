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
  public async Task ExecuteAsync()
  {
    try
    {
      requestedPost.AssertContentIsOfRequiredLength();
      requestedPost.AssertContentContainsNoInappropriateWords();
      await requestedPost.AddToAsync(existingPosts, addingInProgress);
      await requestedPost.NotifyAsync(followers, addingInProgress);
    }
    catch (Exception e)
    {
      addingInProgress.FailedBecauseOf(e);
    }
  }
}