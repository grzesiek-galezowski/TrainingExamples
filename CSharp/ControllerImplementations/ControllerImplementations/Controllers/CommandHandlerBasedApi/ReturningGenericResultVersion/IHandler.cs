using System.Threading.Tasks;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningGenericResultVersion;

public interface IHandler<in T, TResponse>
{
  Task<TResponse> HandleAsync(T command);
}