using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  public interface IIntent
    {
        Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
          CancellationToken cancellationToken);
    }
}