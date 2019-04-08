using System;

namespace BotLogic.States
{
  public class StatesFactory : IStatesFactory
  {
    private readonly IPlayer _player;

    public StatesFactory(IPlayer player)
    {
      _player = player;
    }

    public IState GetState(StateNames stateName)
    {
      if (stateName == StateNames.BeforeGameStarts)
      {
        return new BeforeGameStartsState(_player);
      }
      else if (stateName == StateNames.EnterBrightRoomState)
      {
        return new EnterBrightRoomState(_player);
      }
      else if (stateName == StateNames.AragornAsksAboutFrodosFianceeName)
      {
        return new AragornAsksAboutFrodosFianceeNameState(_player);
      }
      else
      {
          throw new Exception("trolololo");
      }
    }
  }
}