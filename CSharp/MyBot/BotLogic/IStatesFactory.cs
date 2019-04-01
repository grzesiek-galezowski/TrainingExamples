using BotLogic.StateValues;

namespace BotLogic
{
  public interface IStatesFactory
  {
    IState GetState(States.States state);
  }
}