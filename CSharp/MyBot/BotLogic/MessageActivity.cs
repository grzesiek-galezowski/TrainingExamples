using System.Threading.Tasks;

namespace BotLogic
{
  public class MessageActivity
    {
        private readonly IConversationPartner _conversationPartner;
        private readonly string _text;
        private readonly IntentRecognition _intentRecognition;
        private readonly DialogStateMachine _dialogStateMachine;

        public MessageActivity(
            IConversationPartner conversationPartner,
            string activityText, 
            IntentRecognition intentRecognition, 
            DialogStateMachine dialogStateMachine)
        {
            _conversationPartner = conversationPartner;
            _text = activityText;
            _intentRecognition = intentRecognition;
            _dialogStateMachine = dialogStateMachine;
        }

        public async Task HandleAsync()
        {
            var intent = _intentRecognition.From(_text);
            await intent.ApplyToAsync(_dialogStateMachine, _conversationPartner);
            await _conversationPartner.RespondAsync();
        }
    }
}