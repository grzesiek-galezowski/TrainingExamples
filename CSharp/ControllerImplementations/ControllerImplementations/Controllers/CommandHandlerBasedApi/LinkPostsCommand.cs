using Core.Maybe;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    public class LinkPostsCommand
    {
        public string Id1 { get; set; }
        public string Id2 { get; set; }
        public Either<string, ErrorInfo> Result { get; set; }
    }
}