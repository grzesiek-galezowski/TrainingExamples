package ServiceLocatorAntipattern.Outbound;

import ServiceLocatorAntipattern.Interfaces.DataDestination;

public interface OutboundMessage extends DataDestination {
  void sendVia(OutputSocket outputOutputSocket);
}