using System.Threading;
using System.Threading.Tasks;

namespace BotLogic
{
  public interface IDialogContext
    {
      Task GoToAsync(States.States state, IConversationPartner conversationPartner, CancellationToken cancellationToken);
    }
}