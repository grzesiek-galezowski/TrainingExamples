using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultVersion.Add;

public class AddPostCommand
{
  public string Content { get; set; }
  public string Author { get; set; }
}