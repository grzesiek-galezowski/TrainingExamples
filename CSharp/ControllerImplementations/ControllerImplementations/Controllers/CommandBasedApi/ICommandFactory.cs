namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface ICommandFactory
    {
        IPostCommand CreateAddPostCommand(PostDto postDto, IAddingInProgress addingInProgress);
        IPostCommand CreateLinkPostsCommand(string id1, string id2, ILinkingInProgress linkingInProgress);
    }
}