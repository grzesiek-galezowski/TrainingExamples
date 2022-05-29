using Lib;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;

namespace _06_Dialog
{
  public interface IDialogContext
  {
    Task PromptForMissingFields();
    Task SubmitForm();
    bool IsFormComplete();
    void UpdateFormFrom(Prediction data);
    void ClearForm();
    Task MoveTo(IDialogState initial);
    Task PromptForQueryType();
    Task PromptOnlyForMissingFields();
  }

  public class Dialog : IDialogContext
  {
    private readonly LicensePlateQueryForm _form = new();
    private IDialogState _currentState;

    public async Task Initialize()
    {
      await MoveTo(DialogStates.Initial);
    }

    public async Task MoveTo(IDialogState currentState)
    {
      _currentState = currentState;
      await _currentState.OnEnter(this);
    }

    public async Task PromptForQueryType()
    {
      await User.RespondWith("Please specify the query type");
    }

    public async Task PromptOnlyForMissingFields()
    {
      await User.RespondWith("Please specify only the missing data");
    }

    public async Task OnUnknownIntent()
    {
      await User.RespondWith("Sorry, I don't know what you mean");
    }

    public async Task OnCheckLicensePlateIntent(Prediction data)
    {
      await _currentState.OnCheckLicensePlateIntent(data, this);
    }

    public Task PromptForMissingFields()
    {
      return _form.PromptForMissingFields();
    }

    public Task SubmitForm()
    {
      return _form.Submit();
    }

    public bool IsFormComplete()
    {
      return _form.IsComplete();
    }

    public void UpdateFormFrom(Prediction data)
    {
      var queryData = PlateIntent.GetEntitiesFrom(data);
      _form.UpdateWith(queryData);
    }

    public void ClearForm()
    {
      _form.Clear();
    }

    public async Task OnContextFreePlateDataIntent(Prediction data)
    {
      await _currentState.OnContextFreePlateDataIntent(data, this);
    }
  }
}