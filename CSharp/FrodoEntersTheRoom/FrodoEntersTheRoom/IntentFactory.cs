using TddXt.SimpleNlp;

namespace FrodoEntersTheRoom;

public static class IntentFactory
{
  public static IIntent CreateIntentBasedOn(RecognitionResult recognitionResult, IResponse response)
  {
    var dialog = DialogStorage.ReadDialog();

    if (recognitionResult.TopIntent == "KillCharacter")
    {
      return KillCharacterIntent(recognitionResult, dialog, response);
    }
    else
    {
      return new NoIntent(dialog, response);
    }
  }

  private static IIntent KillCharacterIntent(RecognitionResult recognitionResult, IDialog dialog, IResponse response)
  {
    var characterName =
      recognitionResult.Entities.First(
        entity => entity.Entity.Equals(
          EntityName.Value("Character"))).CanonicalForm.ToString();

    return new KillCharacterIntent(
      characterName,
      dialog, //initial dialog state and cast
      response);
  }
}