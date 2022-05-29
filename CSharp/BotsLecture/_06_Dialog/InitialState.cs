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
      context.UpdateFormFrom(data);
      if (context.IsFormComplete())
      {
        await context.SubmitForm();
        await context.MoveTo(DialogStates.Initial);
      }
      else
      {
        await context.MoveTo(DialogStates.WaitingForMissingData);
      }
    }

    public async Task OnContextFreePlateDataIntent(Prediction data, IDialogContext context)
    {
      await context.PromptForQueryType();
    }
  }
}