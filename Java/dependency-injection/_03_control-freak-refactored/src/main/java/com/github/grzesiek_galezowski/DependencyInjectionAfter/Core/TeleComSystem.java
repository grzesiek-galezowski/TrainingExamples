package com.github.grzesiek_galezowski.DependencyInjectionAfter.Core;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound.Inbound;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.Outbound;

public class TeleComSystem {
  private final ProcessingWorkflow processingWorkflow;
  private final Inbound inbound;
  private final Outbound outbound;

  public TeleComSystem(
      Inbound binaryUdpInbound,
      Outbound xmlTcpOutbound,
      ProcessingWorkflow acmeProcessingWorkflow) {
    inbound = binaryUdpInbound;
    outbound = xmlTcpOutbound;
    processingWorkflow = acmeProcessingWorkflow;
  }

  public void start() {
    //should it really be "setDomainLogic()" or more abstract?
    inbound.setDomainLogic(processingWorkflow);
    processingWorkflow.setOutbound(outbound);
    inbound.startListening();
  }
}
