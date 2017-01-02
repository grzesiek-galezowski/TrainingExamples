package ServiceLocatorAntipattern.Outbound;

import ServiceLocatorAntipattern.Interfaces.AcmeMessage;

public interface Outbound {
  void send(AcmeMessage message);
}
