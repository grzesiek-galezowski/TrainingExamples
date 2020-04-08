namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    public class AddPostCommand
    {
        public PostDto Post { get; set; }
        public PostCreatedDto ResponseCreatedPost { get; set; }
    }
}