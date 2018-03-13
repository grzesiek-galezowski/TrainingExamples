package com.randori.inbound.logic;

public interface SubscriptionCommand {
    void validate();

    void execute();
}
