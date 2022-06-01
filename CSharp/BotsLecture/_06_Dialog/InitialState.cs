using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;

namespace _06_Dialog
{
  public class InitialState : IDialogState
  {
    public async Task OnEnter(IDialogContext context)
    {
      context.ClearForm();
    }

    public async Task OnCheckLicensePlateIntent(Prediction data, IDialogContext context)
    {
      await context.EvaluateForm(data);
    }

    public async Task OnContextFreePlateDataIntent(Prediction data, IDialogContext context)
    {
      await context.PromptForQueryType();
    }
  }
}