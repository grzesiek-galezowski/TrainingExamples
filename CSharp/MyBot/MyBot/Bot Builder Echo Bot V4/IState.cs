using System.Threading.Tasks;

public interface IState
{
  Task OnWatchGameCatalogAsync(User user, DialogStateMachine dialogContext);
  Task OnEnterAsync(User user);
  Task OnGoShoppingAsync(User user, DialogStateMachine dialogStateMachine);
  Task OnYesAsync(User user, DialogStateMachine dialogStateMachine);
  Task OnNoAsync(User user, DialogStateMachine dialogStateMachine);
}

class DisplayingGameCatalogState : IState
{
  private readonly GameCatalog _gameCatalog;

  public DisplayingGameCatalogState(GameCatalog gameCatalog)
  {
    _gameCatalog = gameCatalog;
  }

  public Task OnWatchGameCatalogAsync(User user, DialogStateMachine dialogContext)
  {
    user.SayAsync("You are already watching game catalog.");
    return Task.CompletedTask;
  }

  public async Task OnEnterAsync(User user)
  {
    var games = await _gameCatalog.GetGamesAsync();
    games.DisplayFor(user);
  }

  public Task OnGoShoppingAsync(User user, DialogStateMachine dialogStateMachine)
  {
    return dialogStateMachine.GoToAsync(States.FromGameCatalogToDisplayShop, user);
  }

  public Task OnYesAsync(User user, DialogStateMachine dialogStateMachine)
  {
    return user.SayAsync("There's nothing to confirm");
  }

  public Task OnNoAsync(User user, DialogStateMachine dialogStateMachine)
  {
    return user.SayAsync("There's nothing to reject");
  }
}

class InitialChoiceState : IState
{
  public Task OnWatchGameCatalogAsync(User user, DialogStateMachine dialogContext)
  {
    return dialogContext.GoToAsync(States.DisplayingCatalog, user);
  }

  public Task OnEnterAsync(User user)
  {
    return Task.CompletedTask;
  }

  public Task OnGoShoppingAsync(User user, DialogStateMachine dialogStateMachine)
  {
    return dialogStateMachine.GoToAsync(States.DisplayingShop, user);
  }

  public Task OnYesAsync(User user, DialogStateMachine dialogStateMachine)
  {
    return user.SayAsync("There's nothing to confirm");
  }

  public Task OnNoAsync(User user, DialogStateMachine dialogStateMachine)
  {
    return user.SayAsync("There's nothing to reject");
  }
}