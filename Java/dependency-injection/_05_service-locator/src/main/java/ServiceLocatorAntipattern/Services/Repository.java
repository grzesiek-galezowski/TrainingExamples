package ServiceLocatorAntipattern.Services;

import ServiceLocatorAntipattern.Interfaces.Message;

public interface Repository {
  void save(Message message);
}
