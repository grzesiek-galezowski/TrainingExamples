package Commands;

public interface CommandProcessing {
    void ApplyTo(SubscriptionCommand subscriptionStartCommand);
}
