package com.github.TrainingExamples.grzesiek_galezowski.commands;


public class AdapterFromSubscriptionCommandToCommand implements Command {
    private final SubscriptionCommand innerCommand;

    public AdapterFromSubscriptionCommandToCommand(SubscriptionCommand innerCommand) {
        this.innerCommand = innerCommand;
    }

    public void invoke() {
        innerCommand.validateData();
        innerCommand.resolve();
        innerCommand.authorize();
        innerCommand.invoke();
    }
}
