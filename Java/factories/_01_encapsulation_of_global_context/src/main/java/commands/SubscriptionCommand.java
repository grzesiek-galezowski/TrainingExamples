package commands;

public interface SubscriptionCommand extends Command {
    void validateData();

    void authorize();

    void resolve();
}
