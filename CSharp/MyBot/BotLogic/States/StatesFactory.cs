using System;

namespace BotLogic.StateValues
{
  public class StatesFactory : IStatesFactory
  {
    private readonly GameCatalog _gameCatalog;
    private readonly Shop _shop;

    public StatesFactory(GameCatalog gameCatalog, Shop shop)
    {
      _gameCatalog = gameCatalog;
      _shop = shop;
    }

    public IState GetState(States.States state)
    {
      if (state == States.States.InitialChoice)
      {
        return new InitialChoiceState();
      }
      else if(state == States.States.DisplayingCatalog)
      {
        return new DisplayingGameCatalogState(_gameCatalog);
      }
      else if(state == States.States.FromGameCatalogToDisplayShop)
      {
        return new YesNoTransitionState();
      }
      else if(state == States.States.DisplayingShop)
      {
        return new DisplayingShopState(_shop);
      }
      else
      {
          throw new Exception("trolololo");
      }
    }
  }
}