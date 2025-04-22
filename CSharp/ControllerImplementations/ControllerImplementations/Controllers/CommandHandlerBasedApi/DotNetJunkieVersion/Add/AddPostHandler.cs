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
  public async Task HandleAsync(AddPostCommand command)
  {
    try
    {
      postAssertions.AssertContentIsOfRequiredLength(command);
      postAssertions.AssertContentContainsNoInappropriateWords(command);
      command.Result = Either<PostCreatedDto, ErrorInfo>.Result(await existingPosts.AddAsync(command));
      await followers.NotifyAsync(command);
    }
    catch (Exception e)
    {
      command.Result = Either<PostCreatedDto, ErrorInfo>.Error(new ErrorInfo(e));
    }
  }
}