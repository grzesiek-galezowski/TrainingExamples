using System;

namespace BotLogic.States
{
  public class StatesFactory : IStatesFactory
  {
    public IState GetState(StateNames stateName)
    {
      if (stateName == StateNames.BeforeGameStarts)
      {
        return new BeforeGameStartsState();
      }
      else if (stateName == StateNames.EnterBrightRoomState)
      {
        return new EnterBrightRoomState();
      }
      else
      {
          throw new Exception("trolololo");
      }
    }
  }
}