package ServiceLocatorAntipattern.Services;

import ServiceLocatorAntipattern.Interfaces.AcmeMessage;

public interface IRepository {
  void save(AcmeMessage message);
}
