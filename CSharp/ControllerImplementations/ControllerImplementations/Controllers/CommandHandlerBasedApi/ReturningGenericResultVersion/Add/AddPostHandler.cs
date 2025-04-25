using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Add;

public class AddPostHandler<TResponse>(
  IPostAssertions postAssertions,
  IExistingPosts existingPosts,
  IFollowers followers,
  IResponseFactory<TResponse> responseFactory)
  : IHandler<AddPostCommand, TResponse>
{
  public async Task<TResponse> HandleAsync(AddPostCommand command)
  {
    try
    {
      postAssertions.AssertContentIsOfRequiredLength(command);
      postAssertions.AssertContentContainsNoInappropriateWords(command);
      await followers.NotifyAsync(command);
      var postCreatedDto = await existingPosts.AddAsync(command);
      return responseFactory.SavedSuccessfully(postCreatedDto);
    }
    catch (Exception e)
    {
      return responseFactory.FailedBecauseOf(e);
    }
  }
}