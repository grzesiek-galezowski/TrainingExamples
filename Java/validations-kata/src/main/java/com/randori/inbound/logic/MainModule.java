package com.randori.inbound.logic;

import com.randori.inbound.thirdparty.SubscriptionRequestData;

public class MainModule {
    private CommandFactory commandFactory;

    public MainModule(CommandFactory commandFactory) {
        this.commandFactory = commandFactory;
    }

    public void handle(SubscriptionRequestData data) {
        SubscriptionCommand command = this.commandFactory.createFrom(data);
        command.validate();
        command.execute();
    }
}
