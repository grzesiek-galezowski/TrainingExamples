using System.Threading.Tasks;

namespace BotLogic
{
  internal class YesIntent : IIntent
  {
    public Task ApplyTo(DialogStateMachine dialogStateMachine, IUser user)
    {
      return dialogStateMachine.OnNoAsync(user);
    }
  }
}