using System;

namespace BotLogic.Characters
{
  public class Aragorn : ICharacter
  {
    public void TryToKill(IConversationPartner conversationPartner)
    {
      conversationPartner.AppendToResponse(BotPhrases.AttemptingToKillAragornAnswer());
    }
  }
}