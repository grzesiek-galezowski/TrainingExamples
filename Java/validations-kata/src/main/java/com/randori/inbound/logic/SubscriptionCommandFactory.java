package com.randori.inbound.logic;

import com.randori.inbound.thirdparty.SubscriptionRequestData;

public class SubscriptionCommandFactory implements CommandFactory {
    @Override
    public SubscriptionCommand createFrom(SubscriptionRequestData input) {
        SubscriptionStartCommand command = new SubscriptionStartCommand(input,
            new Errors() {
                @Override
                public Boolean hasAny() {
                    //todo implement
                    return null;
                }

                @Override
                public void print() {
                    //todo implement

                }

                @Override
                public void addError(String errorString) {
                    //todo implement

                }
            }, new SubscriptionValidationRules()
        );
        return command;
    }
}
