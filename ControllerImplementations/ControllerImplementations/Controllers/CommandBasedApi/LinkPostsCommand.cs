using System;
using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    internal class LinkPostsCommand : IPostCommand
    {
        private readonly string _id1;
        private readonly string _id2;
        private readonly IExistingPosts _existingPosts;
        private readonly IFollowers _followers;
        private readonly ILinkingInProgress _linkingInProgress;

        public LinkPostsCommand(
            string id1, 
            string id2, 
            IExistingPosts existingPosts,
            IFollowers followers,
            ILinkingInProgress linkingInProgress)
        {
            _id1 = id1;
            _id2 = id2;
            _existingPosts = existingPosts;
            _followers = followers;
            _linkingInProgress = linkingInProgress;
        }

        public async Task ExecuteAsync()
        {
            try
            {
                var rootPost = await _existingPosts.RetrieveByAsync(_id1);
                var linkedPost = await _existingPosts.RetrieveByAsync(_id2);
                rootPost.AssertIsNotTheSameAs(linkedPost);
                rootPost.Link(linkedPost, _linkingInProgress, _followers);
                rootPost.UpdateInAsync(_existingPosts);
            }
            catch (Exception e)
            {
                _linkingInProgress.FailedFor(_id1, _id2, e);
            }
        }
    }
}