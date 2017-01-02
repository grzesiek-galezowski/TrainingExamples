package ServiceLocatorAntipattern.Outbound;

import ServiceLocatorAntipattern.Interfaces.DataDestination;

public interface IOutboundMessage extends DataDestination {
  void sendVia(OutputSocket outputOutputSocket);
}