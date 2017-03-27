package com.github.grzesiek_galezowski.BastardInjection.InMessages;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.BastardInjection.Services.Authorization;

public class StopMessage implements Message {
  public void authorizeUsing(Authorization authorizationRules) {
    System.out.println("Authorizing Stop with " + authorizationRules);
  }

  public void writeTo(DataDestination dataDestination) {
    System.out.println("Writing Stop to " + dataDestination);
  }
}