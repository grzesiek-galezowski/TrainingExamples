using System.Threading;
using System.Threading.Tasks;

namespace BotLogic
{
  public class MessageActivity
    {
        private readonly IConversationPartner _conversationPartner;
        private readonly IIntentRecognition _intentRecognition;
        private readonly IDialogStateMachine _dialogStateMachine;

        public MessageActivity(
            IConversationPartner conversationPartner, 
            IIntentRecognition intentRecognition, 
            IDialogStateMachine dialogStateMachine)
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