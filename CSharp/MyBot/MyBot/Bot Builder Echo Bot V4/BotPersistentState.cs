using System.Threading;
using System.Threading.Tasks;
using Bot_Builder_Echo_Bot_V4;
using Microsoft.Bot.Builder;

public class BotPersistentState
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

  public Task CommitChangesAsync() //bug save it somewhere
  {
    return _accessors.ConversationState.SaveChangesAsync(_turnContext);
  }
}