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
  public async Task<Either<PostCreatedDto, ErrorInfo>> Handle(AddPostCommand command)
  {
    try
    {
      postAssertions.AssertContentIsOfRequiredLength(command);
      postAssertions.AssertContentContainsNoInappropriateWords(command);
      await followers.Notify(command);
      return await existingPosts.Add(command);
    }
    catch (Exception e)
    {
      return new ErrorInfo(e);
    }
  }
}