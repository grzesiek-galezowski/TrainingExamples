using System.Threading.Tasks;

namespace BotLogic.Intents
{
  internal class WatchCatalogIntent : IIntent
  {
    public Task ApplyToAsync(DialogStateMachine dialogStateMachine, IUser user)
    {
      return dialogStateMachine.OnWatchGameCatalogAsync(user);
    }
  }
}