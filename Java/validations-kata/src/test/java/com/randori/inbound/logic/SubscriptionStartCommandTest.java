package com.randori.inbound.logic;

import autofixture.publicinterface.Any;
import com.randori.inbound.thirdparty.SubscriptionRequestData;
import org.testng.annotations.Test;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.never;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;

public class SubscriptionStartCommandTest {

    @Test
    public void testValidateSubscription() throws Exception {
        //given
        SubscriptionRequestData input = Any.anonymous(SubscriptionRequestData.class);
        ValidationRules validationRules = mock(ValidationRules.class);
        Errors errors = mock(Errors.class);
        SubscriptionStartCommand command =
            new SubscriptionStartCommand(input, errors, validationRules);

        //when
        command.validate();

        //then
        verify(validationRules).validateId(input.sessionId, errors);
        verify(validationRules).validateDuration(input.duration, errors);

    }

    @Test
    public void testExecuteSubscription() throws Exception {
        //given
        SubscriptionRequestData input = Any.anonymous(SubscriptionRequestData.class);
        Errors errors = mock(Errors.class);
        SubscriptionStartCommand command =
            new SubscriptionStartCommand(input, errors,
                Any.anonymous(ValidationRules.class));
        when(errors.hasAny()).thenReturn(true);
        //when
        command.execute();
        //then
        verify(errors).print();
    }

    @Test
    public void testExecuteNoErrorsSubscription() throws Exception {
        //given
        SubscriptionRequestData input = Any.anonymous(SubscriptionRequestData.class);
        Errors errors = mock(Errors.class);
        SubscriptionStartCommand command =
            new SubscriptionStartCommand(input, errors, Any.anonymous(ValidationRules.class));
        when(errors.hasAny()).thenReturn(false);
        //when
        command.execute();
        //then
        verify(errors, never()).print();
    }
}