package com.github.grzesiek_galezowski.BastardInjection.InMessages;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.BastardInjection.Services.IAuthorization;

public class StartMessage implements AcmeMessage {
  public void authorizeUsing(IAuthorization authorizationRules) {
    System.out.println("Authorizing start with " + authorizationRules);
  }

  public void writeTo(DataDestination dataDestination) {
    dataDestination.add("Writing start to " + dataDestination);
  }
}
