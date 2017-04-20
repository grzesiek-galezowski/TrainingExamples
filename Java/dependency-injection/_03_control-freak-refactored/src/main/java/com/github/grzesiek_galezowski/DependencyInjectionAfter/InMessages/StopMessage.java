package com.github.grzesiek_galezowski.DependencyInjectionAfter.InMessages;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.Authorization;

public class StopMessage implements AcmeMessage {
    public void authorizeUsing(Authorization authorizationRules) {
        System.out.println("Authorizing Stop with " + authorizationRules);
    }

    public void writeTo(DataDestination dataDestination) {
        System.out.println("Writing Stop to " + dataDestination);
    }
}
