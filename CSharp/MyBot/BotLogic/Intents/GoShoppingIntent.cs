using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class GoShoppingIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnGoShoppingIntentAsync(conversationPartner, cancellationToken);
    }
  }
}