package ServiceLocatorAntipattern.Inbound;

import ServiceLocatorAntipattern.Core.ProcessingWorkflow;

public interface Inbound {
  void setDomainLogic(ProcessingWorkflow processingWorkflow);

  void startListening();
}
