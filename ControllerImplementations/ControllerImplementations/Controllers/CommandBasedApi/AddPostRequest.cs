using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public class AddPostRequest : IAddPostRequest
    {
        private readonly PostDto _postDto;

        public AddPostRequest(PostDto postDto)
        {
            _postDto = postDto;
        }

        public void AssertContentIsOfRequiredLength()
        {
        }

        public void AssertContentContainsNoInappropriateWords()
        {
        }

        public async Task AddToAsync(IExistingPosts existingPosts, IAddingInProgress addingInProgress)
        {
            var id = await existingPosts.AddAsync(_postDto);
            addingInProgress.SavedSuccessfully(_postDto, id);
        }

        public async Task NotifyAsync(IFollowers followers, IAddingInProgress addingInProgress)
        {
        }
    }
}