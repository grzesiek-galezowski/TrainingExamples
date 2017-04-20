package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound.Inbound;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound.Outbound;

public class TeleComSystem {
  private final ProcessingWorkflow processingWorkflow;
  private final Inbound inbound;
  private final Outbound outbound;

  public TeleComSystem() {
    inbound = ApplicationRoot.CONTEXT.resolve(Inbound.class);
    outbound = ApplicationRoot.CONTEXT.resolve(Outbound.class);
    processingWorkflow = ApplicationRoot.CONTEXT.resolve(ProcessingWorkflow.class);
  }

  public void start() {
    inbound.setDomainLogic(processingWorkflow);
    processingWorkflow.setOutbound(outbound);
    inbound.startListening();
  }
}
