package com.github.grzesiek_galezowski.DependencyInjectionAfter.Core;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound.Inbound;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.Outbound;

public class TeleComSystem {
  private final ProcessingWorkflow _processingWorkflow;
  private final Inbound _inbound;
  private final Outbound _outbound;

  public TeleComSystem(
      Inbound binaryUdpInbound,
      Outbound xmlTcpOutbound,
      ProcessingWorkflow acmeProcessingWorkflow) {
    _inbound = binaryUdpInbound;
    _outbound = xmlTcpOutbound;
    _processingWorkflow = acmeProcessingWorkflow;
  }

  public void Start() {
    _inbound.SetDomainLogic(_processingWorkflow);
    _processingWorkflow.SetOutbound(_outbound);
    _inbound.StartListening();
  }
}
