namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IExistingPosts _existingPosts;
        private readonly IFollowers _followers;

        public CommandFactory(IExistingPosts existingPosts, IFollowers followers)
        {
            _existingPosts = existingPosts;
            _followers = followers;
        }

        //TODO demonstrate command dispatcher
        public IPostCommand CreateAddPostCommand(PostDto postDto, IAddingInProgress addingInProgress)
        {
            return new CreatePostCommand(
                new AddPostRequest(postDto), 
                _existingPosts,
                _followers, 
                addingInProgress);
        }

        public IPostCommand CreateLinkPostsCommand(string id1, string id2, ILinkingInProgress linkingInProgress)
        {
            return new LinkPostsCommand(id1, id2, _existingPosts, _followers, linkingInProgress);
        }
    }
}