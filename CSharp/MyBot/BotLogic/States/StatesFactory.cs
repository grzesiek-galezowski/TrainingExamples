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

    public IState GetState(States state)
    {
      if (state == States.InitialChoice)
      {
        return new InitialChoiceState();
      }
      else if(state == States.DisplayingCatalog)
      {
        return new DisplayingGameCatalogState(_gameCatalog);
      }
      else if(state == States.FromGameCatalogToDisplayShop)
      {
        return new YesNoTransitionState();
      }
      else if(state == States.DisplayingShop)
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