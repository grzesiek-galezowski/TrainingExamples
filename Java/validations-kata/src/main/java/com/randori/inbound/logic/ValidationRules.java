package com.randori.inbound.logic;


import java.time.Duration;

public interface ValidationRules {
    void validateId(String sessionId, Errors errors);

    void validateDuration(Duration duration, Errors errors);
}
