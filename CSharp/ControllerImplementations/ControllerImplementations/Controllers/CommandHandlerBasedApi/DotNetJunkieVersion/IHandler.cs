using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public interface IHandler<in T>
{
  Task HandleAsync(T command);
}