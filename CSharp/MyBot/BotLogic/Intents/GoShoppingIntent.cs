using System.Threading.Tasks;

namespace BotLogic
{
  internal class GoShoppingIntent : IIntent
  {
    public Task ApplyTo(DialogStateMachine dialogStateMachine, IUser user)
    {
      return dialogStateMachine.OnGoShoppingIntentAsync(user);
    }
  }
}