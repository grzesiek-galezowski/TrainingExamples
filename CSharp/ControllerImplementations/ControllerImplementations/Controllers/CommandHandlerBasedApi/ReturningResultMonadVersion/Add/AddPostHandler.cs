using System;
using System.Threading.Tasks;
using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Add;

public class AddPostHandler(
  IPostAssertions postAssertions,
  IExistingPosts existingPosts,
  IFollowers followers)
  : IHandler<AddPostCommand, PostCreatedDto, ErrorInfo>
{
  public async Task<Either<PostCreatedDto, ErrorInfo>> HandleAsync(AddPostCommand command)
  {
    try
    {
      postAssertions.AssertContentIsOfRequiredLength(command);
      postAssertions.AssertContentContainsNoInappropriateWords(command);
      await followers.NotifyAsync(command);
      return await existingPosts.AddAsync(command);
    }
    catch (Exception e)
    {
      return new ErrorInfo(e);
    }
  }
}