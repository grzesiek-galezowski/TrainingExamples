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
      IConversationPartner conversationPartner,
      CancellationToken cancellationToken);
  }

  public class ActivityFactory : IActivityFactory
  {
    public async Task<MessageActivity> CreateMessageActivityAsync(
      IBotPersistentState persistentState,
      IUserPhrase userPhrase,
      IConversationPartner conversationPartner,
      CancellationToken cancellationToken)
    {
      IStatesFactory states = new StatesFactory();
      var messageActivity = new MessageActivity(
        conversationPartner,
        new IntentRecognition(userPhrase),
        new DialogStateMachine(
          states.GetState(
            await persistentState.ReadCurrentStateAsync(cancellationToken, StateNames.BeforeGameStarts)),
          states,
          persistentState
        ));
      return messageActivity;
    }
  }
}