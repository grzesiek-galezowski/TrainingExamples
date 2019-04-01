using System.Threading;
using System.Threading.Tasks;
using BotLogic;
using BotLogic.StateValues;

namespace BotBuilderEchoBotV4
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
      IStatesFactory states = new StatesFactory(new GameCatalog(), new Shop());
      var messageActivity = new MessageActivity(
        conversationPartner,
        new IntentRecognition(userPhrase),
        new DialogStateMachine(
          states.GetState(
            await persistentState.ReadCurrentStateAsync(cancellationToken, States.InitialChoice)),
          states,
          persistentState
        ));
      return messageActivity;
    }
  }
}