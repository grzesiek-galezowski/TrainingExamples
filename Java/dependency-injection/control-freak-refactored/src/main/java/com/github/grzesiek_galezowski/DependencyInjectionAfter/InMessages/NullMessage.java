package com.github.grzesiek_galezowski.DependencyInjectionAfter.InMessages;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.IAuthorization;

public class NullMessage implements AcmeMessage {
    public void AuthorizeUsing(IAuthorization authorizationRules) {

    }

    public void WriteTo(DataDestination dataDestination) {

    }
}
