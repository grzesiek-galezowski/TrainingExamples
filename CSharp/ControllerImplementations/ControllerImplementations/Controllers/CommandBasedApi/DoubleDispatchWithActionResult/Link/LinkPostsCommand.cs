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
  public async Task Execute()
  {
    try
    {
      var rootPost = await existingPosts.RetrieveBy(id1);
      var linkedPost = await existingPosts.RetrieveBy(id2);
      rootPost.AssertIsNotTheSameAs(linkedPost);
      rootPost.Link(linkedPost, linkingInProgress, followers);
      rootPost.UpdateIn(existingPosts);
    }
    catch (Exception e)
    {
      linkingInProgress.FailedFor(id1, id2, e);
    }
  }
}