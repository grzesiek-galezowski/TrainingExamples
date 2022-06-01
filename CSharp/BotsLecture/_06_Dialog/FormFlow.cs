using Lib;

namespace _06_Dialog
{
  public class FormFlow
  {
    public async Task Evaluate(LicensePlateQueryForm licensePlateQueryForm, IDialogContext dialog, LicensePlateQueryData queryData)
    {
      licensePlateQueryForm.UpdateWith(queryData);
      if (licensePlateQueryForm.IsComplete())
      {
        await licensePlateQueryForm.Submit();
        await dialog.MoveTo(DialogStates.Initial);
      }
      else
      {
        await dialog.MoveTo(DialogStates.WaitingForMissingData);
      }
    }
  }
}