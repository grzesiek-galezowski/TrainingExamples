using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion.Link;

public class LinkPostsCommand
{
  public string Id1 { get; set; }
  public string Id2 { get; set; }
  public Either<string, ErrorInfo> Result { get; set; }
}