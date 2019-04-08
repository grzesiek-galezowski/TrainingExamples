using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BotLogic.Intents;

namespace BotLogic
{
  public class WordsIntent : IIntent
  {
    private readonly IReadOnlyList<string> _words;

    public WordsIntent(IReadOnlyList<string> words)
    {
      _words = words;
    }

    public Task ApplyToAsync(IDialogStateMachine dialogStateMachine, CancellationToken cancellationToken)
    {
      return dialogStateMachine.OnSomeWordsAsync(cancellationToken, _words);
    }
  }
}