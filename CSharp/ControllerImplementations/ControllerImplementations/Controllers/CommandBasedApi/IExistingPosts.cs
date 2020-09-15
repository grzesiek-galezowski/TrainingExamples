using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface IExistingPosts
    {
        Task<string> AddAsync(PostDto postDto);
        Task<IExistingPost> RetrieveByAsync(string id);
    }
}