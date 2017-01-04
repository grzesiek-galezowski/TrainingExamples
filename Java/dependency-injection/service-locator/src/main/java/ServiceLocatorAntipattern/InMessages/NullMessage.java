package ServiceLocatorAntipattern.InMessages;

import ServiceLocatorAntipattern.Interfaces.AcmeMessage;
import ServiceLocatorAntipattern.Interfaces.DataDestination;
import ServiceLocatorAntipattern.Services.IAuthorization;

public class NullMessage implements AcmeMessage {
  public void authorizeUsing(IAuthorization authorizationRules) {

  }

  public void writeTo(DataDestination dataDestination) {

  }
}