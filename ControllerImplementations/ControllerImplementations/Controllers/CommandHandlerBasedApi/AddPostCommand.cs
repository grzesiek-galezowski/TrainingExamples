using Core.Maybe;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    public class AddPostCommand
    {
        public Either<PostCreatedDto, ErrorInfo> Result { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
    }
}