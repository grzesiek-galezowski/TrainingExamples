package com.github.grzesiek_galezowski.DependencyInjectionAfter.InMessages;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.Authorization;

public class StartMessage implements AcmeMessage {
    public void AuthorizeUsing(Authorization authorizationRules) {
        System.out.println("Authorizing Start with " + authorizationRules);
    }

    public void WriteTo(DataDestination dataDestination) {
        dataDestination.Add("Writing Start to " + dataDestination);
    }
}
