using System;
using System.Collections.Generic;
using System.Linq;
using BotLogic;

namespace ComponentSpecification.Automation
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

    public static RecognitionResultDto TalkTo(string characterName)
    {
      return new RecognitionResultDto()
      {
        Intent = IntentNames.TalkToCharacter,
        Entities = new List<EntityDto>()
        {
          new EntityDto
          {
            Entity = characterName,
            Type = EntityTypes.CharacterName
          }
        }
      };
    }

    public static RecognitionResultDto Words(string words)
    {
      return new RecognitionResultDto
      {
        Intent = IntentNames.Words,
        Entities = words.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(w => 
          new EntityDto
          {
            Entity = w,
            Type = EntityTypes.Word
          }
        ).ToList()
      };

    }
  }
}