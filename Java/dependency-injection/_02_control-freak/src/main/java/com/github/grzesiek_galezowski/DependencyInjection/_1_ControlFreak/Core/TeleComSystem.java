package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Core;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Inbound.BinaryUdpInbound;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Outbound.XmlTcpOutbound;

public class TeleComSystem {
    private final AcmeProcessingWorkflow processingWorkflow;
    private final BinaryUdpInbound inbound;
    private final XmlTcpOutbound outbound;

    public TeleComSystem() {
      inbound = new BinaryUdpInbound();
      outbound = new XmlTcpOutbound();
      processingWorkflow = new AcmeProcessingWorkflow();
    }

    public void start() {
      inbound.setDomainLogic(processingWorkflow);
      processingWorkflow.setOutbound(outbound);
      inbound.startListening();
    }

  }