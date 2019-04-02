using System;

namespace BotLogic.States
{
  public class StatesFactory : IStatesFactory
  {

    public StatesFactory()
    {
    }

    public IState GetState(States.StateNames stateName)
    {
      if (stateName == States.StateNames.BeforeGameStarts)
      {
        return new BeforeGameStartsState();
      }
      else
      {
          throw new Exception("trolololo");
      }
    }
  }
}