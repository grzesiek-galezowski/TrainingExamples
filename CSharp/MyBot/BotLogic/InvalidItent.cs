using System.Threading.Tasks;
using BotLogic.Intents;

namespace BotLogic
{
  public class InvalidItent : IIntent
  {
    public async Task ApplyToAsync(DialogStateMachine dialogStateMachine, IUser user)
    {
      user.AppendToResponse("Invalid intent, sorry!");
    }
  }
}