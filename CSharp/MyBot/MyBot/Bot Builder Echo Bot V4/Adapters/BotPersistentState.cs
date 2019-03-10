using System.Threading;
using System.Threading.Tasks;
using BotLogic;
using Microsoft.Bot.Builder;

namespace BotBuilderEchoBotV4.Logic
{
  public class BotPersistentState : IBotPersistentState
  {
    private readonly ITurnContext _turnContext;
    private readonly EchoBotAccessors _accessors;

    public BotPersistentState(ITurnContext turnContext, EchoBotAccessors accessors)
    {
      _turnContext = turnContext;
      _accessors = accessors;
    }

    public Task<States> ReadCurrentStateAsync()
    {
      return _accessors.CurrentState.GetAsync(_turnContext, () => States.InitialChoice);
    }

    public Task SetCurrentStateAsync(States value)
    {
      return _accessors.CurrentState.SetAsync(_turnContext, value, CancellationToken.None);
    }

    public Task CommittChangesAsync() //bug save it somewhere
    {
      return _accessors.ConversationState.SaveChangesAsync(_turnContext);
    }
  }
}