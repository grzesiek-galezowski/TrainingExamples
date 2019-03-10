using System.Threading.Tasks;

namespace BotLogic
{
  internal class NoIntent : IIntent
  {
    public Task ApplyTo(DialogStateMachine dialogStateMachine, IUser user)
    {
      return dialogStateMachine.OnYesAsync(user);
    }
  }
}