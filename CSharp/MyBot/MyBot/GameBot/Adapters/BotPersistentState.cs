using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;
using Microsoft.Bot.Builder;

namespace GameBot.Adapters
{
  public class BotPersistentState : IBotPersistentState
  {
    private readonly ITurnContext _turnContext;
    private readonly BotAccessors _accessors;

    public BotPersistentState(ITurnContext turnContext, BotAccessors accessors)
    {
      _turnContext = turnContext;
      _accessors = accessors;
    }

    public Task<StateNames> ReadCurrentStateAsync(CancellationToken cancellationToken, StateNames initialChoice)
    {
      return _accessors.CurrentState.GetAsync(_turnContext, () => initialChoice, cancellationToken);
    }

    public Task SetCurrentStateAsync(StateNames value, CancellationToken cancellationToken)
    {
      return _accessors.CurrentState.SetAsync(_turnContext, value, cancellationToken);
    }

    public Task CommitChangesAsync(CancellationToken cancellationToken)
    {
      return _accessors.ConversationState.SaveChangesAsync(_turnContext, cancellationToken: cancellationToken);
    }
  }
}