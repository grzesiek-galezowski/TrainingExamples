using System.Threading;
using System.Threading.Tasks;

namespace BotLogic
{
  public interface IPlayer
  {
    void AppendToResponse(string role, string text);
    Task RespondAsync(CancellationToken cancellationToken);
  }
}