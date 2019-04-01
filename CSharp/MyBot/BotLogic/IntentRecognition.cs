using System.Threading;
using System.Threading.Tasks;
using BotLogic.Intents;

namespace BotLogic
{
  public class IntentRecognition
  {
    private readonly IUserPhrase _userPhrase;

    public IntentRecognition(IUserPhrase userPhrase)
    {
      _userPhrase = userPhrase;
    }

    public async Task<IIntent> PerformAsync(CancellationToken cancellationToken)
    {
      var intentDto = await _userPhrase.RecognizeIntentAsync(cancellationToken);

      if (intentDto.Intent == "catalog")
      {
        return new WatchCatalogIntent();
      }
      else if (intentDto.Intent == "yes")
      {
        return new YesIntent();
      }
      else if (intentDto.Intent == "no")
      {
        return new NoIntent();
      }
      else if(intentDto.Intent == "shop")
      {
        return new GoShoppingIntent();
      }
      else
      {
        return new InvalidItent();
      }

    }
  }
}