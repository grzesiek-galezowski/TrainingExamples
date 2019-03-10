using System.Threading.Tasks;

namespace BotLogic.States
{
  public interface IBotPersistentState
  {
    Task<States> ReadCurrentStateAsync();
    Task SetCurrentStateAsync(States value);

    Task CommittChangesAsync() //bug save it somewhere
      ;
  }
}