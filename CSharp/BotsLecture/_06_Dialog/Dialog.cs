using Lib;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;

namespace _06_Dialog
{
  public interface IDialogContext
  {
    Task PromptForMissingFields();
    Task MoveTo(IDialogState initial);
    Task PromptForQueryType();
    Task PromptOnlyForMissingFields();
    Task EvaluateForm(Prediction data);
    void ClearForm();
  }

  public class Dialog : IDialogContext
  {
    private readonly LicensePlateQueryForm _form = new();
    private IDialogState _currentState = new InvalidState();
    private readonly FormFlow _formFlow = new();

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

    public async Task OnContextFreePlateDataIntent(Prediction data)
    {
      await _currentState.OnContextFreePlateDataIntent(data, this);
    }

    public async Task EvaluateForm(Prediction data)
    {
      await _formFlow.Evaluate(_form, this, PlateIntent.GetEntitiesFrom(data));
    }

    public void ClearForm()
    {
      _form.Clear();
    }
  }
}