namespace FrodoEntersTheRoom;

public class NoIntent : IIntent
{
  private readonly Dialog _dialog;
  private readonly IResponse _response;

  public NoIntent(Dialog dialog, IResponse response)
  {
    _dialog = dialog;
    _response = response;
  }

    public async Task Apply()
    {
        await _dialog.OnUnknownPhrase(_response);
    }
}