package ServiceLocatorAntipattern.Core;

import ServiceLocatorAntipattern.Interfaces.AcmeMessage;
import ServiceLocatorAntipattern.Outbound.Outbound;

public interface ProcessingWorkflow {
  void setOutbound(Outbound outbound);

  void applyTo(AcmeMessage message);
}
