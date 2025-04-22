using ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Add;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public interface IPostAssertions
{
  void AssertContentIsOfRequiredLength(AddPostCommand command);
  void AssertContentContainsNoInappropriateWords(AddPostCommand command);
}