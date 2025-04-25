using System;
using System.Threading.Tasks;
using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Link;

public class LinkPostsHandler(ExistingPosts existingPosts, IFollowers followers) : IHandler<LinkPostsCommand>
{
  public async Task Handle(LinkPostsCommand command)
  {
    try
    {
      var rootPost = await existingPosts.RetrieveBy(command.Id1);
      var linkedPost = await existingPosts.RetrieveBy(command.Id2);
      rootPost.AssertIsNotTheSameAs(linkedPost);
      rootPost.Link(linkedPost, command, followers);
      rootPost.UpdateIn(existingPosts);
    }
    catch (Exception e)
    {
      command.Result = Either<string, ErrorInfo>.Error(new ErrorInfo(e));
    }
  }

}