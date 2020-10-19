namespace FrodoEntersTheRoom
{
  public interface IDialog
  {
    void OnAttemptToKill(string characterName, IResponsePhrase responsePhrase);
  }
}