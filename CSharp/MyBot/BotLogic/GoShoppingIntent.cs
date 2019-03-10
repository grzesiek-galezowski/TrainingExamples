using System.Threading.Tasks;
using BotLogic;

namespace BotBuilderEchoBotV4.Logic
{
  internal class GoShoppingIntent : IIntent
  {
    public Task ApplyTo(DialogStateMachine dialogStateMachine, IUser user)
    {
      return dialogStateMachine.OnGoShoppingIntentAsync(user);
    }
  }
}