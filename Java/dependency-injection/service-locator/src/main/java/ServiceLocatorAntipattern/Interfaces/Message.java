package ServiceLocatorAntipattern.Interfaces;

import ServiceLocatorAntipattern.Services.IAuthorization;

public interface Message {
  void authorizeUsing(IAuthorization authorizationRules);

  void writeTo(DataDestination dataDestination);
}
