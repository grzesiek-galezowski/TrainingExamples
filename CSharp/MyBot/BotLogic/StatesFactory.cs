using System;

namespace BotBuilderEchoBotV4.Logic
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
      else if(state == States.DisplayingShop)
      {
        return new DisplayingShopState();
      }
      else
      {
          throw new Exception("trolololo");
      }
    }
  }
}