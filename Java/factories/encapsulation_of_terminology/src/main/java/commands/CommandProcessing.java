package commands;

public interface CommandProcessing {
    void applyTo(SubscriptionCommand subscriptionStartCommand);
}
