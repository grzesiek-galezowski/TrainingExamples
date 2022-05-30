using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;

namespace _06_Dialog
{
  internal class InvalidState : IDialogState
  {
    public async Task OnEnter(IDialogContext context)
    {
      throw new InvalidOperationException("Programmer error");
    }

    public async Task OnCheckLicensePlateIntent(Prediction data, IDialogContext context)
    {
      throw new InvalidOperationException("Programmer error");
    }

    public async Task OnContextFreePlateDataIntent(Prediction data, IDialogContext context)
    {
      throw new InvalidOperationException("Programmer error");
    }
  }
}