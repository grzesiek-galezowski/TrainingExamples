namespace BotBuilderEchoBotV4.Logic
{
  public interface IStatesFactory
  {
    IState GetState(States state);
  }
}