package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.ProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public class MessageInbound implements Inbound {
  private ProcessingWorkflow _processingWorkflow;
  private final InboundSocket _socket;
  private final Parsing _parsing;

  public MessageInbound(
      InboundSocket udpSocket, Parsing binaryParsing) {
    _socket = udpSocket;
    _parsing = binaryParsing;
  }

  public void SetDomainLogic(ProcessingWorkflow processingWorkflow) {
    _processingWorkflow = processingWorkflow;
  }

  public void StartListening() {
    byte[] frameData = new byte[100];
    while (_socket.Receive(frameData)) {
      AcmeMessage message = _parsing.ResultFor(frameData);
      if (message != null) {
        if (_processingWorkflow != null) {
          _processingWorkflow.ApplyTo(message);
        }
      }
    }
  }
}
