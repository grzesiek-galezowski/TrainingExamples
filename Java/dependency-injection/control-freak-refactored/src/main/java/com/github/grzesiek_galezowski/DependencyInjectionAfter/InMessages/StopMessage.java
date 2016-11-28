package com.github.grzesiek_galezowski.DependencyInjectionAfter.InMessages;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.IAuthorization;

public class StopMessage implements AcmeMessage {
    public void AuthorizeUsing(IAuthorization authorizationRules) {
        System.out.println("Authorizing Stop with " + authorizationRules);
    }

    public void WriteTo(DataDestination dataDestination) {
        System.out.println("Writing Stop to " + dataDestination);
    }
}
