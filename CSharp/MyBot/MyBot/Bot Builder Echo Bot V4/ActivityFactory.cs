using System.Threading.Tasks;
using BotLogic;
using BotLogic.States;

namespace BotBuilderEchoBotV4
{
  public class ActivityFactory
  {
    public async Task<MessageActivity> CreateMessageActivity(
      IBotPersistentState persistentState,
      IConversationPartner conversationPartner,
      string activityText)
    {
      IStatesFactory states = new StatesFactory(new GameCatalog(), new Shop());
      var messageActivity = new MessageActivity(
        conversationPartner,
        activityText,
        new IntentRecognition(),
        new DialogStateMachine(
          states.GetState(
            await persistentState.ReadCurrentStateAsync()),
          states,
          persistentState
        ));
      return messageActivity;
    }
  }
}