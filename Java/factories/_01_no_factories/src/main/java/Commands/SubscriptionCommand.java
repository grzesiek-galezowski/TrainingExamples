package Commands;

public interface SubscriptionCommand extends Command {
    void ValidateData();

    void Authorize();

    void Resolve();
}
