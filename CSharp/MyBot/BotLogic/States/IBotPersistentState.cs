using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.StateValues
{
  public interface IBotPersistentState
  {
    Task<States.States> ReadCurrentStateAsync(CancellationToken cancellationToken, States.States initialChoice);
    Task SetCurrentStateAsync(States.States value, CancellationToken cancellationToken);

    Task CommitChangesAsync(CancellationToken cancellationToken) //bug save it somewhere
      ;
  }
}