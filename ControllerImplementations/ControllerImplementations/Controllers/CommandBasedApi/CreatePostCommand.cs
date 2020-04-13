using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    internal class CreatePostCommand : IPostCommand
    {
        private readonly IAddPostRequest _requestedPost;
        private readonly IExistingPosts _existingPosts;
        private readonly IAddingInProgress _addingInProgress;
        private readonly IFollowers _followers;

        public CreatePostCommand(IAddPostRequest requestedPost,
            IExistingPosts existingPosts,
            IFollowers followers,
            IAddingInProgress addingInProgress)
        {
            _requestedPost = requestedPost;
            _existingPosts = existingPosts;
            _addingInProgress = addingInProgress;
            _followers = followers;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                _requestedPost.AssertContentIsOfRequiredLength();
                _requestedPost.AssertContentContainsNoInappropriateWords();
                await _requestedPost.AddToAsync(_existingPosts, _addingInProgress);
                await _requestedPost.NotifyAsync(_followers, _addingInProgress);
            }
            catch (Exception e)
            {
                _addingInProgress.FailedBecauseOf(e);
            }
        }
    }
}