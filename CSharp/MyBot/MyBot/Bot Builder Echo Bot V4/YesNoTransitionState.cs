using System.Threading.Tasks;

namespace Bot_Builder_Echo_Bot_V4
{
  public class YesNoTransitionState : IState //bug rename
  {
    public async Task OnWatchGameCatalogAsync(User user, DialogStateMachine dialogContext)
    {
      await user.SayAsync("OK, let's stay with the catalog");
      await dialogContext.GoToAsync(States.DisplayingCatalog, user);
    }

    public Task OnEnterAsync(User user)
    {
      return user.SayAsync("Do you really want to stop viewing the catalog and go shopping?");
    }

    public async Task OnGoShoppingAsync(User user, DialogStateMachine dialogStateMachine)
    {
      await user.SayAsync("OK, let's go shopping!");
      await dialogStateMachine.GoToAsync(States.DisplayingShop, user);
    }

    public Task OnYesAsync(User user, DialogStateMachine dialogStateMachine)
    {
      return OnGoShoppingAsync(user, dialogStateMachine);
    }

    public Task OnNoAsync(User user, DialogStateMachine dialogStateMachine)
    {
      return OnWatchGameCatalogAsync(user, dialogStateMachine);
    }
  }
}