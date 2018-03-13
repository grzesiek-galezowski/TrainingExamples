package com.randori.inbound.logic;

import autofixture.publicinterface.Any;
import org.testng.annotations.Test;

import java.time.Duration;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.never;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.verifyNoMoreInteractions;

public class SubscriptionValidationRulesTest {

    @Test
    public void whenValidateIdNullValueThenErrorsExists() {
        //GIVEN
        SubscriptionValidationRules validationRules = new SubscriptionValidationRules();
        Errors errors = mock(Errors.class);

        //WHEN
        validationRules.validateId(null, errors);

        //THEN
        verify(errors).addError("no null allowed");
        verifyNoMoreInteractions(errors);
    }

    @Test
    public void subscriptionIdCannotBeEmpty() {
        //GIVEN
        SubscriptionValidationRules validationRules = new SubscriptionValidationRules();
        Errors errors = mock(Errors.class);

        //WHEN
        validationRules.validateId("", errors);

        //THEN
        verify(errors).addError("id cannot be empty");
        verifyNoMoreInteractions(errors);
    }

    @Test
    public void shouldNotAddAnyError_whenSubscriptionIdIsValid() {
        //GIVEN
        SubscriptionValidationRules validationRules = new SubscriptionValidationRules();
        Errors errors = mock(Errors.class);

        //WHEN
        validationRules.validateId(Any.string(), errors);

        //THEN
        verify(errors, never()).addError(any());
    }

    @Test
    public void shouldValidateDuration_whenDurationIsNegative() {
        //GIVEN
        SubscriptionValidationRules validationRules = new SubscriptionValidationRules();
        Errors errors = mock(Errors.class);

        //WHEN
        validationRules.validateDuration(Duration.ofNanos(-1L), errors);

        //THEN
        verify(errors).addError("should not be negative");
        verifyNoMoreInteractions(errors);
    }

    @Test
    public void shouldValidateDuration_whenDurationIsEqualZero() {
        //GIVEN
        SubscriptionValidationRules validationRules = new SubscriptionValidationRules();
        Errors errors = mock(Errors.class);

        //WHEN
        validationRules.validateDuration(Duration.ZERO, errors);

        //THEN
        verify(errors).addError("should not be equal zero");
        verifyNoMoreInteractions(errors);
    }
}