using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class YesIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner)
    {
      return dialogStateMachine.OnNoAsync(conversationPartner);
    }
  }
}