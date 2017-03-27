package ServiceLocatorAntipattern.Interfaces;

import ServiceLocatorAntipattern.Services.Authorization;

public interface Message {
  void authorizeUsing(Authorization authorizationRules);

  void writeTo(DataDestination dataDestination);
}
