package com.github.grzesiek_galezowski.ConformingContainerAntipattern.InMessages;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.DataDestination;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services.Authorization;

public class NullMessage implements InboundMessage {
  public void authorizeUsing(Authorization authorizationRules) {

  }

  public void writeTo(DataDestination dataDestination) {

  }
}
