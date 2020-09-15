using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandBasedApi
{
    public interface IPostCommand
    {
        Task ExecuteAsync();
    }
}