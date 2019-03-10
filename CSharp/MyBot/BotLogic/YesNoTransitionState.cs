using System.Threading.Tasks;

namespace BotLogic
{
  public class YesNoTransitionState : IState //bug rename
  {
    public async Task OnWatchGameCatalogAsync(IUser user, DialogStateMachine dialogContext)
    {
      await user.AppendToResponseAsync("OK, let's stay with the catalog");
      await dialogContext.GoToAsync(States.DisplayingCatalog, user);
    }

    public Task OnEnterAsync(IUser user)
    {
      return user.AppendToResponseAsync("Do you really want to stop viewing the catalog and go shopping?");
    }

    public async Task OnGoShoppingAsync(IUser user, DialogStateMachine dialogStateMachine)
    {
      await user.AppendToResponseAsync("OK, let's go shopping!");
      await dialogStateMachine.GoToAsync(States.DisplayingShop, user);
    }

    public Task OnYesAsync(IUser user, DialogStateMachine dialogStateMachine)
    {
      return OnGoShoppingAsync(user, dialogStateMachine);
    }

    public Task OnNoAsync(IUser user, DialogStateMachine dialogStateMachine)
    {
      return OnWatchGameCatalogAsync(user, dialogStateMachine);
    }
  }
}