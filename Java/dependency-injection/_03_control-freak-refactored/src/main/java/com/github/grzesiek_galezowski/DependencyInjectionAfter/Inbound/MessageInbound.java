package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.ProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public class MessageInbound implements Inbound {
  private ProcessingWorkflow _processingWorkflow;
  private final InboundSocket socket;
  private final Parsing parsing;

  public MessageInbound(
      InboundSocket udpSocket, Parsing binaryParsing) {
    socket = udpSocket;
    parsing = binaryParsing;
  }

  public void SetDomainLogic(ProcessingWorkflow processingWorkflow) {
    _processingWorkflow = processingWorkflow;
  }

  public void StartListening() {
    byte[] frameData = new byte[100];
    while (socket.Receive(frameData)) {
      AcmeMessage message = parsing.ResultFor(frameData);
      if (message != null) {
        if (_processingWorkflow != null) {
          _processingWorkflow.ApplyTo(message);
        }
      }
    }
  }
}
