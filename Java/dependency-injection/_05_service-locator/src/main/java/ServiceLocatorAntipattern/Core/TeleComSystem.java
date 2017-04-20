package ServiceLocatorAntipattern.Core;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Inbound.Inbound;
import ServiceLocatorAntipattern.Outbound.Outbound;

public class TeleComSystem {
  private final ProcessingWorkflow processingWorkflow;
  private final Inbound inbound;
  private final Outbound outbound;

  public TeleComSystem() {
    inbound = ApplicationRoot.context.getComponent(Inbound.class);
    outbound = ApplicationRoot.context.getComponent(Outbound.class);
    processingWorkflow = ApplicationRoot.context.getComponent(ProcessingWorkflow.class);
  }

  public void start() {
    inbound.setDomainLogic(processingWorkflow);
    processingWorkflow.setOutbound(outbound);
    inbound.startListening();
  }
}
