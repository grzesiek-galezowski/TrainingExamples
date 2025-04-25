using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

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