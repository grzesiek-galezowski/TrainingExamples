using System;
using System.Threading.Tasks;
using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Link;

public class LinkPostsHandler(ExistingPosts existingPosts, IFollowers followers) : IHandler<LinkPostsCommand>
{
  public async Task HandleAsync(LinkPostsCommand command)
  {
    try
    {
      var rootPost = await existingPosts.RetrieveByAsync(command.Id1);
      var linkedPost = await existingPosts.RetrieveByAsync(command.Id2);
      rootPost.AssertIsNotTheSameAs(linkedPost);
      rootPost.Link(linkedPost, command, followers);
      rootPost.UpdateInAsync(existingPosts);
    }
    catch (Exception e)
    {
      command.Result = Either<string, ErrorInfo>.Error(new ErrorInfo(e));
    }
  }

}