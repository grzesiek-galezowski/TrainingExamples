using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class NoIntent : IIntent
  {
    public Task ApplyToAsync(IDialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnNoAsync(conversationPartner, cancellationToken);
    }
  }
}