using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class GoShoppingIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner)
    {
      return dialogStateMachine.OnGoShoppingIntentAsync(conversationPartner);
    }
  }
}