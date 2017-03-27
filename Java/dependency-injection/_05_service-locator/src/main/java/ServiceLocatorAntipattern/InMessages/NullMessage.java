package ServiceLocatorAntipattern.InMessages;

import ServiceLocatorAntipattern.Interfaces.Message;
import ServiceLocatorAntipattern.Interfaces.DataDestination;
import ServiceLocatorAntipattern.Services.IAuthorization;

public class NullMessage implements Message {
  public void authorizeUsing(IAuthorization authorizationRules) {

  }

  public void writeTo(DataDestination dataDestination) {

  }
}