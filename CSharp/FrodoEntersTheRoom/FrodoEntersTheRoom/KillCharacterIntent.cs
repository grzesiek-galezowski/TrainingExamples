using System;

namespace FrodoEntersTheRoom;

public class KillCharacterIntent : IIntent
{
  private readonly string _characterName;
  private readonly IDialog _dialog;
  private readonly IResponse _response;

  public KillCharacterIntent(string characterName, IDialog dialog, IResponse response)
  {
    _characterName = characterName;
    _dialog = dialog;
    _response = response;
  }

  public async Task Apply()
  {
    await _dialog.OnAttemptToKill(_characterName, _response);
  }
}