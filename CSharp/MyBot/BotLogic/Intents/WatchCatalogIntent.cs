using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class WatchCatalogIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnWatchGameCatalogAsync(conversationPartner, cancellationToken);
    }
  }
}