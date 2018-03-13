package com.randori.inbound.logic;

import com.randori.inbound.thirdparty.SubscriptionRequestData;

public class SubscriptionStartCommand implements SubscriptionCommand {
    private final SubscriptionRequestData input;
    private final Errors errors;
    private final ValidationRules validationRules;

    public SubscriptionStartCommand(SubscriptionRequestData input, Errors errors, ValidationRules validationRules) {
        this.input = input;
        this.errors = errors;
        this.validationRules = validationRules;
    }

    @Override
    public void validate() {
        validationRules.validateId(input.sessionId, errors);
        validationRules.validateDuration(input.duration, errors);
    }

    @Override
    public void execute() {
        if (errors.hasAny()){
            errors.print();
        }
    }
}
