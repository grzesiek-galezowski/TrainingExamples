using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion.Add;

public class AddPostCommand
{
  public Either<PostCreatedDto, ErrorInfo> Result { get; set; }
  public string Content { get; set; }
  public string Author { get; set; }
}