using ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion;

public interface IPostAssertions
{
  void AssertContentIsOfRequiredLength(AddPostCommand command);
  void AssertContentContainsNoInappropriateWords(AddPostCommand command);
}