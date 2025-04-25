using System;
using System.Threading.Tasks;
using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Link;

public class LinkPostsHandler(ExistingPosts existingPosts, IFollowers followers) : IHandler<LinkPostsCommand, string, ErrorInfo>
{
  public async Task<Either<string, ErrorInfo>> Handle(LinkPostsCommand command)
  {
    try
    {
      var rootPost = await existingPosts.RetrieveBy(command.Id1);
      var linkedPost = await existingPosts.RetrieveBy(command.Id2);
      rootPost.AssertIsNotTheSameAs(linkedPost);
      rootPost.Link(linkedPost, command, followers);
      rootPost.UpdateIn(existingPosts);
      return "Success";
    }
    catch (Exception e)
    {
      return new ErrorInfo(e);
    }
  }

}