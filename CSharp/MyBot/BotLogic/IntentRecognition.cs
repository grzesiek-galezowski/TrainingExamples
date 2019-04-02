using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotLogic.Intents;

namespace BotLogic
{
    public interface IIntentRecognition
    {
        Task<IIntent> PerformAsync(CancellationToken cancellationToken);
    }

    public class IntentRecognition : IIntentRecognition
    {
      private readonly IUserPhrase _userPhrase;

      public IntentRecognition(IUserPhrase userPhrase)
      {
        _userPhrase = userPhrase;
      }

      public async Task<IIntent> PerformAsync(CancellationToken cancellationToken)
      {
        var intentDto = await _userPhrase.RecognizeIntentAsync(cancellationToken);

        if (intentDto.Intent == IntentNames.StartGame)
        {
          return new StartGameIntent();
        }
        if (intentDto.Intent == IntentNames.KillCharacter)
        {
          //todo add validation of entity type
          return new KillCharacterIntent(intentDto.Entities.First().Entity);
        }

        return new InvalidItent();
      }

      public class StartGameIntent : IIntent
      {
        public Task ApplyToAsync(IDialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
          CancellationToken cancellationToken)
        {
          return dialogStateMachine.OnStartGameAsync(conversationPartner, cancellationToken);
        }
      }
    }

    public class KillCharacterIntent : IIntent
    {
      private readonly string _characterName;

      public KillCharacterIntent(string characterName)
      {
        _characterName = characterName;
      }

      public Task ApplyToAsync(IDialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
        CancellationToken cancellationToken)
      {
        return dialogStateMachine.OnKillCharacterAsync(_characterName, conversationPartner, cancellationToken);
      }
    }
}