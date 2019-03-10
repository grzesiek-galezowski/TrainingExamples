using System.Threading.Tasks;
using BotBuilderEchoBotV4.Logic;

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