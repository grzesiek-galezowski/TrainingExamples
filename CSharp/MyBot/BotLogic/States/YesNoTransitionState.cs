using System.Threading.Tasks;

namespace BotLogic.States
{
  public class YesNoTransitionState : IState //bug rename
  {
    public Task OnWatchGameCatalogAsync(IUser user, IDialogContext dialogContext)
    {
      user.AppendToResponse("OK, let's stay with the catalog");
      return dialogContext.GoToAsync(States.DisplayingCatalog, user);
    }

    public async Task OnEnterAsync(IUser user)
    {
      user.AppendToResponse("Do you really want to stop viewing the catalog and go shopping?");
    }

    public Task OnGoShoppingAsync(IUser user, IDialogContext dialogStateMachine)
    {
      user.AppendToResponse("OK, let's go shopping!");
      return dialogStateMachine.GoToAsync(States.DisplayingShop, user);
    }

    public Task OnYesAsync(IUser user, IDialogContext dialogStateMachine)
    {
      return OnGoShoppingAsync(user, dialogStateMachine);
    }

    public Task OnNoAsync(IUser user, IDialogContext dialogStateMachine)
    {
      return OnWatchGameCatalogAsync(user, dialogStateMachine);
    }
  }
}