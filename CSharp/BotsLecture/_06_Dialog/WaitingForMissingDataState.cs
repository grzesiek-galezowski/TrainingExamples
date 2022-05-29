using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;

namespace _06_Dialog
{
  public class WaitingForMissingDataState : IDialogState
  {
    public async Task OnCheckLicensePlateIntent(Prediction data, IDialogContext context)
    {
      await context.PromptOnlyForMissingFields();
    }

    public Task OnContextFreePlateDataIntent(Prediction data, IDialogContext context)
    {
      throw new NotImplementedException();
    }

    public async Task OnEnter(IDialogContext context)
    {
      await context.PromptForMissingFields();
    }
  }
}