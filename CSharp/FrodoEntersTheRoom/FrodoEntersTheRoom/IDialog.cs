namespace FrodoEntersTheRoom;

public interface IDialog
{
  Task OnAttemptToKill(string characterName, IResponse response);
}