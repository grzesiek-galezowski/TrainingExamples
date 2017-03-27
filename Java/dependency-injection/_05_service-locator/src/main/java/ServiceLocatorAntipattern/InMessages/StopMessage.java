package ServiceLocatorAntipattern.InMessages;

import ServiceLocatorAntipattern.Interfaces.Message;
import ServiceLocatorAntipattern.Interfaces.DataDestination;
import ServiceLocatorAntipattern.Services.IAuthorization;

public class StopMessage implements Message {
  public void authorizeUsing(IAuthorization authorizationRules) {
    System.out.println("Authorizing Stop with " + authorizationRules);
  }

  public void writeTo(DataDestination dataDestination) {
    System.out.println("Writing Stop to " + dataDestination);
  }
}