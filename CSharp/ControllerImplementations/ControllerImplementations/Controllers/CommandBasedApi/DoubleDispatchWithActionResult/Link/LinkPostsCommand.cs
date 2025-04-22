using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi.DoubleDispatchWithActionResult.Link;

internal class LinkPostsCommand(
  string id1,
  string id2,
  IExistingPosts existingPosts,
  IFollowers followers,
  ILinkingInProgress linkingInProgress)
  : IPostCommand
{
  public async Task ExecuteAsync()
  {
    try
    {
      var rootPost = await existingPosts.RetrieveByAsync(id1);
      var linkedPost = await existingPosts.RetrieveByAsync(id2);
      rootPost.AssertIsNotTheSameAs(linkedPost);
      rootPost.Link(linkedPost, linkingInProgress, followers);
      rootPost.UpdateInAsync(existingPosts);
    }
    catch (Exception e)
    {
      linkingInProgress.FailedFor(id1, id2, e);
    }
  }
}