package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound.Inbound;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound.Outbound;

public class TeleComSystem {
  private final ProcessingWorkflow _processingWorkflow;
  private final Inbound _inbound;
  private final Outbound _outbound;

  public TeleComSystem() {
    _inbound = ApplicationRoot.CONTEXT.resolve(Inbound.class);
    _outbound = ApplicationRoot.CONTEXT.resolve(Outbound.class);
    _processingWorkflow = ApplicationRoot.CONTEXT.resolve(ProcessingWorkflow.class);
  }

  public void start() {
    _inbound.setDomainLogic(_processingWorkflow);
    _processingWorkflow.setOutbound(_outbound);
    _inbound.startListening();
  }
}
