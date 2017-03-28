package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.ProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public class MessageInbound implements Inbound {
  private ProcessingWorkflow processingWorkflow;
  private final InboundSocket socket;
  private final Parsing parsing;

  public MessageInbound(
      InboundSocket udpSocket, Parsing binaryParsing) {
    socket = udpSocket;
    parsing = binaryParsing;
  }

  public void setDomainLogic(ProcessingWorkflow processingWorkflow) {
    this.processingWorkflow = processingWorkflow;
  }

  public void startListening() {
    byte[] frameData = new byte[100];
    while (socket.receive(frameData)) {
      AcmeMessage message = parsing.resultFor(frameData);
      if (message != null) {
        if (processingWorkflow != null) {
          processingWorkflow.applyTo(message);
        }
      }
    }
  }
}
