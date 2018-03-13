package com.randori.inbound.logic;

import java.time.Duration;

public class ValidationRulesImpl implements ValidationRules {
    @Override
    public void validateId(String sessionId, Errors errors) {
        //todo implement

    }

    @Override
    public void validateDuration(Duration duration, Errors errors) {

        //todo implement
    }

    public void main(String[] args) {
        new MainModule(new SubscriptionCommandFactory());
    }
}
