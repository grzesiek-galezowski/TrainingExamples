using System;

namespace FrodoEntersTheRoom;

public class Dialog : IDialog
{
  private DialogState _dialogState = DialogState.EnteredTheRoom;

  public async Task OnAttemptToKill(string characterName, IResponse response)
  {
    switch (_dialogState) 
    {
      case DialogState.EnteredTheRoom:
        await response.Respond("GameOver", _dialogState);
        _dialogState = DialogState.GameOver;
        break;
      case DialogState.GameOver:
        await response.Respond("No, GameOver!", _dialogState);
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }
  }

  public async Task OnUnknownPhrase(IResponse response)
  {
    switch (_dialogState)
    {
      case DialogState.GameOver:
        await response.Respond("Not sure I understand you, but it's game over anyway, so...", _dialogState);
        break;
      default:
        await response.Respond("Not sure I understand you", _dialogState);
        break;
    }
  }
}