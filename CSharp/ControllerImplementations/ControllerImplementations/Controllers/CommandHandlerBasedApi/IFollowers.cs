using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    public interface IFollowers
    {
        Task NotifyAsync(AddPostCommand command);
    }
}