using System.Threading.Tasks;

namespace BotLogic
{
  public interface IBotPersistentState
  {
    Task<States> ReadCurrentStateAsync();
    Task SetCurrentStateAsync(States value);

    Task CommittChangesAsync() //bug save it somewhere
      ;
  }
}