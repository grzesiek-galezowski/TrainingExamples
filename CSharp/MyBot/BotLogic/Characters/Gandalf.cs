namespace BotLogic.Characters
{
  public interface ICharacter
  {
    void TryToKill(IConversationPartner conversationPartner);
  }

  public class Gandalf : ICharacter
  {
    public void TryToKill(IConversationPartner conversationPartner)
    {
      conversationPartner.AppendToResponse(BotPhrases.AttemptingToKillGandalfAnswer());
    }
  }
}