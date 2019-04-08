using BotLogic.States;

namespace BotLogic
{
  public interface IStatesFactory
  {
    IState GetState(StateNames stateName);
  }
}