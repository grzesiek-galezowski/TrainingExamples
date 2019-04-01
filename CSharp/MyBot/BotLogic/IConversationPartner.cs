using System.Threading;
using System.Threading.Tasks;

namespace BotLogic
{
  public interface IConversationPartner
  {
    void AppendToResponse(string text);
    Task RespondAsync(CancellationToken cancellationToken);
  }
}