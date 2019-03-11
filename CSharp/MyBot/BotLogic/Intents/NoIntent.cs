using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class NoIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner)
    {
      return dialogStateMachine.OnYesAsync(conversationPartner);
    }
  }
}