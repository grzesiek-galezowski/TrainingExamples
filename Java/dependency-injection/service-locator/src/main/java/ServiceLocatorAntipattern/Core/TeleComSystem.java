package ServiceLocatorAntipattern.Core;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Inbound.IInbound;
import ServiceLocatorAntipattern.Outbound.Outbound;

public class TeleComSystem {
  private final ProcessingWorkflow _processingWorkflow;
  private final IInbound _inbound;
  private final Outbound _outbound;

  public TeleComSystem() {
    _inbound = ApplicationRoot.context.getComponent(IInbound.class);
    _outbound = ApplicationRoot.context.getComponent(Outbound.class);
    _processingWorkflow = ApplicationRoot.context.getComponent(ProcessingWorkflow.class);
  }

  public void start() {
    _inbound.setDomainLogic(_processingWorkflow);
    _processingWorkflow.setOutbound(_outbound);
    _inbound.startListening();
  }
}
