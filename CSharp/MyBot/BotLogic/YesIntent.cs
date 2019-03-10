using System.Threading.Tasks;
using BotLogic;

namespace BotBuilderEchoBotV4.Logic
{
  internal class YesIntent : IIntent
  {
    public Task ApplyTo(DialogStateMachine dialogStateMachine, IUser user)
    {
      return dialogStateMachine.OnNoAsync(user);
    }
  }
}