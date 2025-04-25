using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Link;

public class LinkPostsHandler<TResponse>(
  ExistingPosts existingPosts,
  IFollowers followers,
  IResponseFactory<TResponse> responseFactory) : IHandler<LinkPostsCommand, TResponse>
{
  public async Task<TResponse> Handle(LinkPostsCommand command)
  {
    try
    {
      var rootPost = await existingPosts.RetrieveBy(command.Id1);
      var linkedPost = await existingPosts.RetrieveBy(command.Id2);
      rootPost.AssertIsNotTheSameAs(linkedPost);
      rootPost.Link(linkedPost, command, followers);
      rootPost.UpdateIn(existingPosts);
      return responseFactory.LinkedSuccessfully();
    }
    catch (Exception e)
    {
      return responseFactory.FailedBecauseOf(e);
    }
  }
}