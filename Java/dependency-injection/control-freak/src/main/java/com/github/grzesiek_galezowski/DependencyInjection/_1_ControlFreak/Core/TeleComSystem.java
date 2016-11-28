package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Core;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Inbound.BinaryUdpInbound;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Outbound.XmlTcpOutbound;

public class TeleComSystem {
    private final AcmeProcessingWorkflow _processingWorkflow;
    private final BinaryUdpInbound _inbound;
    private final XmlTcpOutbound _outbound;

    public TeleComSystem() {
      _inbound = new BinaryUdpInbound();
      _outbound = new XmlTcpOutbound();
      _processingWorkflow = new AcmeProcessingWorkflow();
    }

    public void start() {
      _inbound.setDomainLogic(_processingWorkflow);
      _processingWorkflow.setOutbound(_outbound);
      _inbound.startListening();
    }

  }