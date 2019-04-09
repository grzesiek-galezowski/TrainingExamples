using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
      private readonly IPlayer _player;

      public IntentRecognition(IUserPhrase userPhrase, IPlayer player)
      {
        _userPhrase = userPhrase;
        _player = player;
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
        if (intentDto.Intent == IntentNames.Words)
        {
          //todo add validation of entity type
          return new WordsIntent(Words.From(EntityValuesIn(intentDto).ToImmutableList()));
        }

        return new InvalidItent(_player); //bug is that even needed?
      }

      private static IEnumerable<string> EntityValuesIn(RecognitionResultDto intentDto)
      {
        return intentDto.Entities.Select(e => e.Entity);
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

    }
}