using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class YesIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnNoAsync(conversationPartner, cancellationToken);
    }
  }
}