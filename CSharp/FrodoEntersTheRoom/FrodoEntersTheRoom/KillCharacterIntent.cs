using System;

namespace FrodoEntersTheRoom
{
  public class KillCharacterIntent
  {
    private readonly string _characterName;
    private readonly IDialog _dialog;
    private readonly IResponsePhrase _responsePhrase;

    public KillCharacterIntent(string characterName, IDialog dialog, IResponsePhrase responsePhrase)
    {
      _characterName = characterName;
      _dialog = dialog;
      _responsePhrase = responsePhrase;
    }

    public void Apply()
    {
      _dialog.OnAttemptToKill(_characterName, _responsePhrase);
    }
  }
}