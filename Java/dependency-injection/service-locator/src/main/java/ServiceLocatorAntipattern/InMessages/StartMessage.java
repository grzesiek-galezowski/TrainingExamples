package ServiceLocatorAntipattern.InMessages;

import ServiceLocatorAntipattern.Interfaces.AcmeMessage;
import ServiceLocatorAntipattern.Interfaces.DataDestination;
import ServiceLocatorAntipattern.Services.IAuthorization;

public class StartMessage implements AcmeMessage {
  public void authorizeUsing(IAuthorization authorizationRules) {
    System.out.println("Authorizing start with " + authorizationRules);
  }

  public void writeTo(DataDestination dataDestination) {
    dataDestination.add("Writing start to " + dataDestination);
  }
}
