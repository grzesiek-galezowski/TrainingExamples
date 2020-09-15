using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface IAddPostRequest
    {
        void AssertContentIsOfRequiredLength();
        void AssertContentContainsNoInappropriateWords();
        Task AddToAsync(IExistingPosts existingPosts, IAddingInProgress addingInProgress);
        Task NotifyAsync(IFollowers followers, IAddingInProgress addingInProgress);
    }
}