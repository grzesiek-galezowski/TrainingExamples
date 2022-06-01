using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;

namespace _06_Dialog
{
  public interface IDialogState
  {
    Task OnEnter(IDialogContext context);
    Task OnCheckLicensePlateIntent(Prediction data, IDialogContext context);
    Task OnContextFreePlateDataIntent(Prediction data, IDialogContext context);
  }
}