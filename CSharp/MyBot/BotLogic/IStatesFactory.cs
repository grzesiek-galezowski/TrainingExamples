namespace BotLogic
{
  public interface IStatesFactory
  {
    IState GetState(States state);
  }
}