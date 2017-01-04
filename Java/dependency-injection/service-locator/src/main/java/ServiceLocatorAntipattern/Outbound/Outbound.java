package ServiceLocatorAntipattern.Outbound;

import ServiceLocatorAntipattern.Interfaces.Message;

public interface Outbound {
  void send(Message message);
}
