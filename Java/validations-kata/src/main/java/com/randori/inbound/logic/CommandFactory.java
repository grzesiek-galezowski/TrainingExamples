package com.randori.inbound.logic;

import com.randori.inbound.thirdparty.SubscriptionRequestData;

public interface CommandFactory {
    SubscriptionCommand createFrom(SubscriptionRequestData input);
}
