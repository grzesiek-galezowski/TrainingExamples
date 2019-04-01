using System.Threading;
using System.Threading.Tasks;
using BotLogic.StateValues;
using Microsoft.Bot.Builder;

namespace BotBuilderEchoBotV4.Adapters
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

    public Task<States> ReadCurrentStateAsync(CancellationToken cancellationToken, States initialChoice)
    {
      return _accessors.CurrentState.GetAsync(_turnContext, () => initialChoice, cancellationToken);
    }

    public Task SetCurrentStateAsync(States value, CancellationToken cancellationToken)
    {
      return _accessors.CurrentState.SetAsync(_turnContext, value, cancellationToken);
    }

    public Task CommitChangesAsync(CancellationToken cancellationToken)
    {
      return _accessors.ConversationState.SaveChangesAsync(_turnContext, cancellationToken: cancellationToken);
    }
  }
}