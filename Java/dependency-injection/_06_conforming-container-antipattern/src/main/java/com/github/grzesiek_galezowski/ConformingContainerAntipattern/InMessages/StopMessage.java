package com.github.grzesiek_galezowski.ConformingContainerAntipattern.InMessages;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services.Authorization;


public class StopMessage implements InboundMessage {
  public void authorizeUsing(Authorization authorizationRules) {
    System.out.println("Authorizing Stop with " + authorizationRules);
  }

  public void writeTo(DataDestination dataDestination) {
    System.out.println("Writing Stop to " + dataDestination);
  }
}
