package com.github.grzesiek_galezowski.DependencyInjectionAfter.Core;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound.IInbound;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.IOutbound;

public class TeleComSystem {
  private final IAcmeProcessingWorkflow _processingWorkflow;
  private final IInbound _inbound;
  private final IOutbound _outbound;

  public TeleComSystem(
      IInbound binaryUdpInbound,
      IOutbound xmlTcpOutbound,
      IAcmeProcessingWorkflow acmeProcessingWorkflow) {
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
