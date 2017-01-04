package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Inbound;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Core.AcmeProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.AcmeMessage;

public class BinaryUdpInbound {
    private AcmeProcessingWorkflow _processingWorkflow;
    private final UdpSocket _socket;
    private final BinaryParsing _parsing;

    public BinaryUdpInbound() {
      _socket = new UdpSocket();
      _parsing = new BinaryParsing();
    }

    public void setDomainLogic(AcmeProcessingWorkflow processingWorkflow) {
      _processingWorkflow = processingWorkflow;
    }

    public void startListening() {
      byte[] frameData = new byte[100];
      while (_socket.receive(frameData)) {
        AcmeMessage message = _parsing.resultFor(frameData);
        if (message != null) {
          if (_processingWorkflow != null) {
            _processingWorkflow.applyTo(message);
          }
        }
      }
    }
  }
