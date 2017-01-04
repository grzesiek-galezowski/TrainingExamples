package com.github.grzesiek_galezowski.ConformingContainerAntipattern.InMessages;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services.Authorization;

public class StartMessage implements InboundMessage {
  public void authorizeUsing(Authorization authorizationRules) {
    System.out.println("Authorizing start with " + authorizationRules);
  }

  public void writeTo(DataDestination dataDestination) {
    dataDestination.add("Writing start to " + dataDestination);
  }
}
