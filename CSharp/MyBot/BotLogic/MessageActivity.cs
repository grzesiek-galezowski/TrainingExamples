using System.Threading;
using System.Threading.Tasks;

namespace BotLogic
{
  public class MessageActivity
    {
        private readonly IPlayer _player;
        private readonly IIntentRecognition _intentRecognition;
        private readonly IDialogStateMachine _dialogStateMachine;

        public MessageActivity(
            IPlayer player, 
            IIntentRecognition intentRecognition, 
            IDialogStateMachine dialogStateMachine)
        {
            _player = player;
            _intentRecognition = intentRecognition;
            _dialogStateMachine = dialogStateMachine;
        }

        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            var intent = await _intentRecognition.PerformAsync(cancellationToken);
            await intent.ApplyToAsync(_dialogStateMachine, cancellationToken);
        }
    }
}