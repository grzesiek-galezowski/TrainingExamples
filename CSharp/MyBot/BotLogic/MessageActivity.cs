using System.Threading;
using System.Threading.Tasks;

namespace BotLogic
{
  public class MessageActivity
    {
        private readonly IConversationPartner _conversationPartner;
        private readonly IntentRecognition _intentRecognition;
        private readonly DialogStateMachine _dialogStateMachine;

        public MessageActivity(
            IConversationPartner conversationPartner, 
            IntentRecognition intentRecognition, 
            DialogStateMachine dialogStateMachine)
        {
            _conversationPartner = conversationPartner;
            _intentRecognition = intentRecognition;
            _dialogStateMachine = dialogStateMachine;
        }

        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            var intent = await _intentRecognition.PerformAsync(cancellationToken);
            await intent.ApplyToAsync(_dialogStateMachine, _conversationPartner, cancellationToken);
        }
    }
}