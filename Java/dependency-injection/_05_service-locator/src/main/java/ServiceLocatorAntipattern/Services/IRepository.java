package ServiceLocatorAntipattern.Services;

import ServiceLocatorAntipattern.Interfaces.Message;

public interface IRepository {
  void save(Message message);
}
