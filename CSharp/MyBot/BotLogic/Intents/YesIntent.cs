using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class YesIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IUser user)
    {
      return dialogStateMachine.OnNoAsync(user);
    }
  }
}