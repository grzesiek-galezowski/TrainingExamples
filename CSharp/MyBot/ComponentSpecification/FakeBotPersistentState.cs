using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;

namespace ComponentSpecification
{
  public class FakeBotPersistentState : IBotPersistentState
  {
    private StateNames? _stateName;

    public async Task<StateNames> ReadCurrentStateAsync(CancellationToken cancellationToken, StateNames initialChoice)
    {
      return _stateName ?? initialChoice;
    }

    public async Task SetCurrentStateAsync(StateNames value, CancellationToken cancellationToken)
    {
      _stateName = value;
    }

    public Task CommitChangesAsync(CancellationToken cancellationToken) //bug remove from this interface?
    {
      return Task.CompletedTask;
    }
  }
}