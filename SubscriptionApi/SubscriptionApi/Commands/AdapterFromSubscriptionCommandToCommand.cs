using SubscriptionApi.Dto;

namespace SubscriptionApi.Commands
{
  public interface CommandProcessing
  {
    void ApplyTo(SubscriptionCommand subscriptionStartCommand);
  }

  public class AdapterFromSubscriptionCommandToCommand : Command
  {
    private readonly SubscriptionCommand _innerCommand;

    public AdapterFromSubscriptionCommandToCommand(SubscriptionCommand innerCommand)
    {
      _innerCommand = innerCommand;
    }

    public void Invoke()
    {
      _innerCommand.ValidateData();
      _innerCommand.Resolve();
      _innerCommand.Authorize();
      _innerCommand.Invoke();
    }
  }
}