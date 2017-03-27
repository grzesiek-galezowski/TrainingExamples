package com.github.grzesiek_galezowski.BastardInjection.InMessages;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.BastardInjection.Services.Authorization;

public class StartMessage implements Message {
  public void authorizeUsing(Authorization authorizationRules) {
    System.out.println("Authorizing start with " + authorizationRules);
  }

  public void writeTo(DataDestination dataDestination) {
    dataDestination.add("Writing start to " + dataDestination);
  }
}
