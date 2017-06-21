namespace SubscriptionApi.Commands
{
  public interface Command
  {
    void Invoke();
  }

  public interface SubscriptionCommand : Command
  {
    void ValidateData();
    void Authorize();
    void Resolve();
  }
}