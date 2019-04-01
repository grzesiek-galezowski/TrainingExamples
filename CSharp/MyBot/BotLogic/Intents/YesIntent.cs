using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class YesIntent : IIntent
  {
    public Task ApplyToAsync(
        IDialogStateMachine dialogStateMachine, 
        IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnYesAsync(conversationPartner, cancellationToken);
    }
  }
}