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
      var recognitionResultDto = new RecognitionResultDto()
      {
        Intent = luisResult.TopScoringIntent.Intent,
        Entities = luisResult.Entities.Select(
          e => new EntityDto
        {
          Entity = e.Entity,
          Type = e.Type,
        }).ToList(),
      };
      return recognitionResultDto;
    }
  }
}