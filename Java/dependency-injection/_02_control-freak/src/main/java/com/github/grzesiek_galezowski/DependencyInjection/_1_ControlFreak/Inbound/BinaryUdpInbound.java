package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Inbound;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Core.AcmeProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.AcmeMessage;

public class BinaryUdpInbound {
    private AcmeProcessingWorkflow workflow;
    private final UdpSocket socket;
    private final BinaryParsing parsing;

    public BinaryUdpInbound() {
      socket = new UdpSocket();
      parsing = new BinaryParsing();
    }

    public void setDomainLogic(AcmeProcessingWorkflow processingWorkflow) {
      workflow = processingWorkflow;
    }

    public void startListening() {
      byte[] frameData = new byte[100];
      while (socket.receive(frameData)) {
        AcmeMessage message = parsing.resultFor(frameData);
        if (message != null) {
          if (workflow != null) {
            workflow.applyTo(message);
          }
        }
      }
    }
  }
