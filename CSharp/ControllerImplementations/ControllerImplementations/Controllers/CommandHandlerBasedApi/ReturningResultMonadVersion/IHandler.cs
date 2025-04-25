using System.Threading.Tasks;
using Core.Either;

namespace ControllerImplementations.Controllers.CommandHandlerBasedApi.ReturningResultMonadVersion;

public interface IHandler<in T, TResult, TError>
{
  Task<Either<TResult, TError>> Handle(T command);
}