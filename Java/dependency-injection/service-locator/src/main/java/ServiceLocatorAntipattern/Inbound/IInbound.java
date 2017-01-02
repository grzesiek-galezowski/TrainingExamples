package ServiceLocatorAntipattern.Inbound;

import ServiceLocatorAntipattern.Core.ProcessingWorkflow;

public interface IInbound {
  void setDomainLogic(ProcessingWorkflow processingWorkflow);

  void startListening();
}
