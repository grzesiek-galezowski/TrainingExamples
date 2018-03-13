package com.randori.inbound.logic;


import java.time.Duration;

public class SubscriptionValidationRules implements ValidationRules {
    @Override
    public void validateId(String sessionId, Errors errors) {
        if (sessionId == null) {
            errors.addError("no null allowed");
        } else if("".equals(sessionId)) {
            errors.addError("id cannot be empty");
        }
    }

    @Override
    public void validateDuration(Duration duration, Errors errors) {
        if(duration.isNegative()) {
            errors.addError("should not be negative");
        } else {
            errors.addError("should not be equal zero");
        }
    }
}
