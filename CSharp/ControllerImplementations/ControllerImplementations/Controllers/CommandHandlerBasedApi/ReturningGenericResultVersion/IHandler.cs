using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

public interface IHandler<in T, TResponse>
{
  Task<TResponse> Handle(T command);
}