using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class WatchCatalogIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner)
    {
      return dialogStateMachine.OnWatchGameCatalogAsync(conversationPartner);
    }
  }
}