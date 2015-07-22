namespace SubscriptionApi.Commands
{
  public interface CommandProcessing
  {
    void ApplyTo(SubscriptionStartCommand subscriptionStartCommand);
    void ApplyTo(SubscriptionStopCommand subscriptionStopCommand);
  }

  public class CommandFromApiProcessing : CommandProcessing
  {
    public void ApplyTo(SubscriptionStartCommand subscriptionStartCommand)
    {
      subscriptionStartCommand.ValidateData();
      subscriptionStartCommand.Resolve();
      subscriptionStartCommand.Authorize();
      subscriptionStartCommand.Invoke();
    }

    public void ApplyTo(SubscriptionStopCommand subscriptionStopCommand)
    {
      subscriptionStopCommand.ValidateData();
      subscriptionStopCommand.Authorize();
      subscriptionStopCommand.Invoke();
    } //bug maybe these two methods can be joined?
  }
}