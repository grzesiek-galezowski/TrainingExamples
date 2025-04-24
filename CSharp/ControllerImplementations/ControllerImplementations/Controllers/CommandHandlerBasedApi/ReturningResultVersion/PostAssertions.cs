using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion;

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