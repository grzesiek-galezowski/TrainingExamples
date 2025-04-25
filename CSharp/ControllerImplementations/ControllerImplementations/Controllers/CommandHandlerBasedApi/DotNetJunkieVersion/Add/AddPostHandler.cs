using System;
using System.Threading.Tasks;
using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Add;

public class AddPostHandler(
  IPostAssertions postAssertions,
  IExistingPosts existingPosts,
  IFollowers followers)
  : IHandler<AddPostCommand>
{
  public async Task Handle(AddPostCommand command)
  {
    try
    {
      postAssertions.AssertContentIsOfRequiredLength(command);
      postAssertions.AssertContentContainsNoInappropriateWords(command);
      command.Result = Either<PostCreatedDto, ErrorInfo>.Result(await existingPosts.Add(command));
      await followers.Notify(command);
    }
    catch (Exception e)
    {
      command.Result = Either<PostCreatedDto, ErrorInfo>.Error(new ErrorInfo(e));
    }
  }
}