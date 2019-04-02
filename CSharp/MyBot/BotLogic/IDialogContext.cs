using System.Threading;
using System.Threading.Tasks;

namespace BotLogic
{
  public interface IDialogContext
    {
      Task GoToAsync(States.StateNames stateName, IConversationPartner conversationPartner, CancellationToken cancellationToken);
    }
}