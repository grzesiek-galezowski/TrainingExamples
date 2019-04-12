using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BotLogic;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;

namespace Adapters.Luis
{

  public class LuisUserPhrase : IUserPhrase
  {
    private readonly ITurnContext _turnContext;
    private readonly LuisRecognizer _luisRecognizer;

    public LuisUserPhrase(ITurnContext turnContext, LuisRecognizer luisRecognizer)
    {
      _turnContext = turnContext;
      _luisRecognizer = luisRecognizer;
    }

    public async Task<RecognitionResultDto> RecognizeIntentAsync(CancellationToken cancellationToken)
    {
      var recognizerResult = await _luisRecognizer.RecognizeAsync(_turnContext, cancellationToken);

      var luisResult = (LuisResult)recognizerResult.Properties["luisResult"];
        var topScoringIntent = luisResult.TopScoringIntent.Intent;
        var recognitionResultDto = new RecognitionResultDto()
        {
          Intent = topScoringIntent,
          Entities = EntitiesExtractedFrom(luisResult),
        };

      return recognitionResultDto;
    }

    private static List<EntityDto> EntitiesExtractedFrom(LuisResult luisResult)
    {
      if (luisResult.TopScoringIntent.Intent == IntentNames.None)
      {
        return luisResult.Query.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(word =>
          new EntityDto
          {
            Entity = word,
            Type = EntityTypes.Word,
          }
        ).ToList();
      }
      return luisResult.Entities.Select(
        e => new EntityDto
        {
          Entity = e.Entity,
          Type = e.Type,
        }).ToList();
    }
  }
}