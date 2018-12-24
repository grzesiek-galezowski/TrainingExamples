using System.Threading.Tasks;

namespace Bot_Builder_Echo_Bot_V4
{
  public class StatesFactory : IStatesFactory
  {
    public IState GetState(States state)
    {
      if (state == States.InitialChoice)
      {
        return new InitialChoiceState();
      }
      else if(state == States.DisplayingCatalog)
      {
        return new DisplayingGameCatalogState(new GameCatalog());
      }
      else if(state == States.FromGameCatalogToDisplayShop)
      {
        return new YesNoTransitionState();
      }
      else //if(state == States.DisplayingShop)
      {
        return new DisplayingShopState();
      }
    }
  }

  public class DisplayingShopState : IState
  {
    public Task OnWatchGameCatalogAsync(User user, DialogStateMachine dialogContext)
    {
      throw new System.NotImplementedException();
    }

    public Task OnEnterAsync(User user)
    {
      throw new System.NotImplementedException();
    }

    public Task OnGoShoppingAsync(User user, DialogStateMachine dialogStateMachine)
    {
      throw new System.NotImplementedException();
    }

    public Task OnYesAsync(User user, DialogStateMachine dialogStateMachine)
    {
      throw new System.NotImplementedException();
    }

    public Task OnNoAsync(User user, DialogStateMachine dialogStateMachine)
    {
      throw new System.NotImplementedException();
    }
  }
}