using System.Threading.Tasks;

namespace BotLogic.States
{
  public class InitialChoiceState : IState
  {
    public Task OnWatchGameCatalogAsync(IUser user, IDialogContext dialogContext)
    {
      return dialogContext.GoToAsync(States.DisplayingCatalog, user);
    }

    public Task OnEnterAsync(IUser user)
    {
      return Task.CompletedTask;
    }

    public Task OnGoShoppingAsync(IUser user, IDialogContext dialogStateMachine)
    {
      return dialogStateMachine.GoToAsync(States.DisplayingShop, user);
    }

    public async Task OnYesAsync(IUser user, IDialogContext dialogStateMachine)
    {
      user.AppendToResponse("There's nothing to confirm");
    }

    public async Task OnNoAsync(IUser user, IDialogContext dialogStateMachine)
    {
      user.AppendToResponse("There's nothing to reject");
    }

  }
}
