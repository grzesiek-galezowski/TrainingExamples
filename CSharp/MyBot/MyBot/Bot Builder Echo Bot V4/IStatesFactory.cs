using BotBuilderEchoBotV4.Navigation;

namespace BotBuilderEchoBotV4
{
  public interface IStatesFactory
  {
    IState GetState(States state);
  }
}