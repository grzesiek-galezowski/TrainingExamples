namespace BotLogic.States
{
  public interface ICharacter
  {
    void TryToKill(IConversationPartner conversationPartner);
  }

  public class Gandalf : ICharacter
  {
    public void TryToKill(IConversationPartner conversationPartner)
    {
      conversationPartner.AppendToResponse(BrightRoomConversations.AttemptingToKillGandalfAnswer());
    }
  }
}