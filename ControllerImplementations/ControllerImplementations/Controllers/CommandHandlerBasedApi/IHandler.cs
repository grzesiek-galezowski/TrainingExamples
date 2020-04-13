using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi
{
    public interface IHandler<in T>
    {
        Task HandleAsync(T command);
    }
}