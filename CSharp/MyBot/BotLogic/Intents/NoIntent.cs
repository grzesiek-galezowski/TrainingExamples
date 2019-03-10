using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class NoIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IUser user)
    {
      return dialogStateMachine.OnYesAsync(user);
    }
  }
}