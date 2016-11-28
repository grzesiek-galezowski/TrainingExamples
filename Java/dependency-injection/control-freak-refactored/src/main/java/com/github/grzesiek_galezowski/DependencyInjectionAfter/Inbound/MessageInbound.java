package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.IAcmeProcessingWorkflow;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public class MessageInbound implements IInbound {
  private IAcmeProcessingWorkflow _processingWorkflow;
  private final IInboundSocket _socket;
  private final IParsing _parsing;

  public MessageInbound(
      IInboundSocket udpSocket, IParsing binaryParsing) {
    _socket = udpSocket;
    _parsing = binaryParsing;
  }

  public void SetDomainLogic(IAcmeProcessingWorkflow processingWorkflow) {
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
