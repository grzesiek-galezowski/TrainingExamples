package ServiceLocatorAntipattern.InMessages;

import ServiceLocatorAntipattern.Interfaces.Message;
import ServiceLocatorAntipattern.Interfaces.DataDestination;
import ServiceLocatorAntipattern.Services.Authorization;

public class NullMessage implements Message {
  public void authorizeUsing(Authorization authorizationRules) {

  }

  public void writeTo(DataDestination dataDestination) {

  }
}