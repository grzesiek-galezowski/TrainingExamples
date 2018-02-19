package Commands;


public class AdapterFromSubscriptionCommandToCommand implements Command {
    private final SubscriptionCommand _innerCommand;

    public AdapterFromSubscriptionCommandToCommand(SubscriptionCommand innerCommand) {
        _innerCommand = innerCommand;
    }

    public void Invoke() {
        _innerCommand.ValidateData();
        _innerCommand.Resolve();
        _innerCommand.Authorize();
        _innerCommand.Invoke();
    }
}
