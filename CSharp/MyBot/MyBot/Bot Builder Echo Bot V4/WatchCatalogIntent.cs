using System.Threading.Tasks;

internal class WatchCatalogIntent : IIntent
{
  private readonly string _text;

  public WatchCatalogIntent(string text)
  {
    _text = text;
  }

  public Task ApplyTo(DialogStateMachine dialogStateMachine, User user)
  {
    return dialogStateMachine.OnWatchGameCatalogAsync(user);
  }
}