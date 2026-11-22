namespace DemoUi.Logic;

public interface IStateChangeObserver
{
  Task Refresh();
}

public class NullStateChangeObserver : IStateChangeObserver
{
  public Task Refresh()
  {
    return Task.CompletedTask;
  }
}