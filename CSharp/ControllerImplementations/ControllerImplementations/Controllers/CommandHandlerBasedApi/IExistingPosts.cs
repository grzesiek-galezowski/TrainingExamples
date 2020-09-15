using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    public interface IExistingPosts
    {
        Task<PostCreatedDto> AddAsync(AddPostCommand command);
    }
}