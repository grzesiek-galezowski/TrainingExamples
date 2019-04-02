using System.Collections.Generic;
using BotLogic;

namespace ComponentSpecification
{
  public static class Intents
  {
    public static RecognitionResultDto StartGame()
    {
      return new RecognitionResultDto
      {
        Intent = IntentNames.StartGame
      };
    }

    public static RecognitionResultDto Kill(string character)
    {
      return new RecognitionResultDto()
      {
        Intent = IntentNames.KillCharacter,
        Entities = new List<EntityDto>()
        {
          new EntityDto
          {
            Entity = character,
            Type = EntityTypes.CharacterName
          }
        }
      };
    }
  }
}