package com.randori.inbound.logic;

import autofixture.publicinterface.Any;
import com.randori.inbound.thirdparty.SubscriptionRequestData;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class SubscriptionCommandFactoryTest {

    @Test
    public void factoryTest() {
        //GIVEN
        CommandFactory factory = new SubscriptionCommandFactory();
        SubscriptionRequestData input = Any.anonymous(SubscriptionRequestData.class);
        //WHEN
        SubscriptionCommand command = factory.createFrom(input);
        //THEN
        assertThat(command).isInstanceOf(SubscriptionStartCommand.class);
    }

}