using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion;

public class PostAssertions : IPostAssertions
{
  public void AssertContentIsOfRequiredLength(AddPostCommand command)
  {
    throw new System.NotImplementedException();
  }

  public void AssertContentContainsNoInappropriateWords(AddPostCommand command)
  {
    throw new System.NotImplementedException();
  }
}