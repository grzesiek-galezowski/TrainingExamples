using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class GoShoppingIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IUser user)
    {
      return dialogStateMachine.OnGoShoppingIntentAsync(user);
    }
  }
}