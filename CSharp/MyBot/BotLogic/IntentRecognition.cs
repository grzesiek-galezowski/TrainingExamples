using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotLogic.Characters;
using BotLogic.Intents;
using BotLogic.States;

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
          return new KillCharacterIntent(ExtractCharacterFrom(intentDto));
        }
        if (intentDto.Intent == IntentNames.TalkToCharacter)
        {
          //todo add validation of entity type
          return new TalkToCharacterIntent(ExtractCharacterFrom(intentDto));
        }

        return new InvalidItent();
      }

      private static ICharacter ExtractCharacterFrom(RecognitionResultDto intentDto)
      {
        return GetCharacter(intentDto.Entities.First(e => e.Type == "CharacterName"));
      }

      private static ICharacter GetCharacter(EntityDto entity)
      {
        if (entity.Entity.Equals("Gandalf", StringComparison.InvariantCultureIgnoreCase))
        {
          return new Gandalf();
        }
        else
        {
          return new Aragorn();
        }
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

    public class TalkToCharacterIntent : IIntent
    {
      public TalkToCharacterIntent(ICharacter character)
      {
        throw new NotImplementedException();
      }

      public Task ApplyToAsync(IDialogStateMachine dialogStateMachine, IConversationPartner conversationPartner,
        CancellationToken cancellationToken)
      {
        throw new NotImplementedException();
      }
    }
}