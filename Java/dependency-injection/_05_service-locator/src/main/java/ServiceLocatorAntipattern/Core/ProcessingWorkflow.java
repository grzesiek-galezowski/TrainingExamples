package ServiceLocatorAntipattern.Core;

import ServiceLocatorAntipattern.Interfaces.Message;
import ServiceLocatorAntipattern.Outbound.Outbound;

public interface ProcessingWorkflow {
  void setOutbound(Outbound outbound);

  void applyTo(Message message);
}
