using System.Threading.Tasks;
using BotLogic.Intents;

namespace BotLogic
{
  public class InvalidItent : IIntent
  {
    public async Task ApplyToAsync(DialogStateMachine dialogStateMachine, IConversationPartner conversationPartner)
    {
      conversationPartner.AppendToResponse("Invalid intent, sorry!");
    }
  }
}