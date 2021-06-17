using System.Threading.Tasks;

namespace IoCContainerRefactoring.Controllers
{
  public interface IWeatherCommand
  {
    Task Execute();
  }
}