using System;
using System.Threading;
using System.Threading.Tasks;
using ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Link;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithWritingToHttpContext.Add;

internal class CreatePostCommand(
  IAddPostRequest requestedPost,
  IExistingPosts existingPosts,
  IFollowers followers,
  IAddingInProgress addingInProgress)
  : IPostCommand
{
  public async Task Execute(CancellationToken token)
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
      await addingInProgress.FailedBecauseOf(e);
    }
  }
}