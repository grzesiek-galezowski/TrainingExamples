using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class NoIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnYesAsync(conversationPartner, cancellationToken);
    }
  }
}