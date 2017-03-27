package ServiceLocatorAntipattern.InMessages;

import ServiceLocatorAntipattern.Interfaces.Message;
import ServiceLocatorAntipattern.Interfaces.DataDestination;
import ServiceLocatorAntipattern.Services.Authorization;

public class StartMessage implements Message {
  public void authorizeUsing(Authorization authorizationRules) {
    System.out.println("Authorizing start with " + authorizationRules);
  }

  public void writeTo(DataDestination dataDestination) {
    dataDestination.add("Writing start to " + dataDestination);
  }
}
