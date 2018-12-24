using System.Threading.Tasks;

internal class MessageActivity
{
  private readonly User _user;
  private readonly string _text;
  private readonly IntentRecognition _intentRecognition;
  private readonly DialogStateMachine _dialogStateMachine;

  public MessageActivity(
    User user,
    string activityText, 
    IntentRecognition intentRecognition, 
    DialogStateMachine dialogStateMachine)
  {
    _user = user;
    _text = activityText;
    _intentRecognition = intentRecognition;
    _dialogStateMachine = dialogStateMachine;
  }

  public async Task HandleAsync()
  {
    var intent = _intentRecognition.From(_text);
    await intent.ApplyTo(_dialogStateMachine, _user);
    await _user.RespondAsync();
  }
}