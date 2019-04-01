using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
  public interface IBotPersistentState
  {
    Task<States> ReadCurrentStateAsync(CancellationToken cancellationToken, States initialChoice);
    Task SetCurrentStateAsync(States value, CancellationToken cancellationToken);

    Task CommitChangesAsync(CancellationToken cancellationToken) //bug save it somewhere
      ;
  }
}