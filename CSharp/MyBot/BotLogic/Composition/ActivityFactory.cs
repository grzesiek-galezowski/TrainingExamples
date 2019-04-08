using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;

namespace BotLogic.Composition
{
  public interface IActivityFactory
  {
    Task<MessageActivity> CreateMessageActivityAsync(
      IBotPersistentState persistentState,
      IUserPhrase userPhrase,
      IPlayer player,
      CancellationToken cancellationToken);
  }

  public class ActivityFactory : IActivityFactory
  {
    public async Task<MessageActivity> CreateMessageActivityAsync(
      IBotPersistentState persistentState,
      IUserPhrase userPhrase,
      IPlayer player,
      CancellationToken cancellationToken)
    {
      var messageActivity = new MessageActivity(
        player,
        new IntentRecognition(userPhrase, player),
        await DialogStateMachine(persistentState, cancellationToken, new StatesFactory(player)));
      return messageActivity;
    }

    private static async Task<DialogStateMachine> DialogStateMachine(IBotPersistentState persistentState, CancellationToken cancellationToken, IStatesFactory states)
    {
      return new DialogStateMachine(
        states.GetState(
          await persistentState.ReadCurrentStateAsync(cancellationToken, StateNames.BeforeGameStarts)),
        states,
        persistentState
      );
    }
  }
}