using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Link;

public class LinkPostsHandler<TResponse>(
  ExistingPosts existingPosts,
  IFollowers followers,
  IResponseFactory<TResponse> responseFactory) : IHandler<LinkPostsCommand, TResponse>
{
  public async Task<TResponse> HandleAsync(LinkPostsCommand command)
  {
    try
    {
      var rootPost = await existingPosts.RetrieveByAsync(command.Id1);
      var linkedPost = await existingPosts.RetrieveByAsync(command.Id2);
      rootPost.AssertIsNotTheSameAs(linkedPost);
      rootPost.Link(linkedPost, command, followers);
      rootPost.UpdateInAsync(existingPosts);
      return responseFactory.LinkedSuccessfully();
    }
    catch (Exception e)
    {
      return responseFactory.FailedBecauseOf(e);
    }
  }
}