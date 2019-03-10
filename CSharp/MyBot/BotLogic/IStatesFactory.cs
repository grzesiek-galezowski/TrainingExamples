using BotLogic.States;

namespace BotLogic
{
  public interface IStatesFactory
  {
    IState GetState(States.States state);
  }
}