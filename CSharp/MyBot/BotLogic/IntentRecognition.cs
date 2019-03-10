namespace BotLogic
{
  public class IntentRecognition
  {
    public IIntent From(string text)
    {
      if (text.Contains("catalog"))
      {
        return new WatchCatalogIntent(text);
      }
      else if (text.Contains("yes"))
      {
        return new YesIntent();
      }
      else if (text.Contains("no"))
      {
        return new NoIntent();
      }
      else
      {
        return new GoShoppingIntent();
      }

    }
  }
}