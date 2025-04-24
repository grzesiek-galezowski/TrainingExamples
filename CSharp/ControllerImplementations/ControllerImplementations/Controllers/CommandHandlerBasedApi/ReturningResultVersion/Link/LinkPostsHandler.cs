using System;
using System.Threading.Tasks;
using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Link;

public class LinkPostsHandler(ExistingPosts existingPosts, IFollowers followers) : IHandler<LinkPostsCommand, string, ErrorInfo>
{
  public async Task<Either<string, ErrorInfo>> HandleAsync(LinkPostsCommand command)
  {
    try
    {
      var rootPost = await existingPosts.RetrieveByAsync(command.Id1);
      var linkedPost = await existingPosts.RetrieveByAsync(command.Id2);
      rootPost.AssertIsNotTheSameAs(linkedPost);
      rootPost.Link(linkedPost, command, followers);
      rootPost.UpdateInAsync(existingPosts);
      return "Success";
    }
    catch (Exception e)
    {
      return new ErrorInfo(e);
    }
  }

}