package ServiceLocatorAntipattern.Interfaces;

import ServiceLocatorAntipattern.Services.IAuthorization;

public interface AcmeMessage {
  void authorizeUsing(IAuthorization authorizationRules);

  void writeTo(DataDestination dataDestination);
}
