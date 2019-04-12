using System;
using System.Threading;
using System.Threading.Tasks;

namespace BotLogic.Intents
{
  public class QuestionWhoIntent : IIntent
  {
    public Task ApplyToAsync(IDialogStateMachine dialogStateMachine, CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnQuestionWhoAsync(cancellationToken);
    }
  }
}