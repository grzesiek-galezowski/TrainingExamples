namespace FrodoEntersTheRoom;

public interface IResponse
{
  Task Respond(string text, DialogState dialogState);
}