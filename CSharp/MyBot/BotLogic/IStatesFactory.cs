using BotLogic.StateValues;

namespace BotLogic
{
  public interface IStatesFactory
  {
    IState GetState(StateValues.States state);
  }
}