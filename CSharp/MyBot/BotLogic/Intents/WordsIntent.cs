using System.Threading;
using System.Threading.Tasks;
using BotLogic.States;

namespace BotLogic.Intents
{
  public class WordsIntent : IIntent
  {
    private readonly Words _words;

    public WordsIntent(Words words)
    {
      _words = words;
    }

    public Task ApplyToAsync(IDialogStateMachine dialogStateMachine, CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnSomeWordsAsync(_words, cancellationToken);
    }
  }
}