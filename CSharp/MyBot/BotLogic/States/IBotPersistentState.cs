using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.States
{
  public interface IBotPersistentState
  {
    Task<States.StateNames> ReadCurrentStateAsync(CancellationToken cancellationToken, States.StateNames initialChoice);
    Task SetCurrentStateAsync(States.StateNames value, CancellationToken cancellationToken);

    Task CommitChangesAsync(CancellationToken cancellationToken) //bug save it somewhere
      ;
  }
}