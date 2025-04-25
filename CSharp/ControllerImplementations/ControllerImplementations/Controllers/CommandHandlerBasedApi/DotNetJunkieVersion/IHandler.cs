using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.DotNetJunkieVersion;

public interface IHandler<in T>
{
  Task Handle(T command);
}