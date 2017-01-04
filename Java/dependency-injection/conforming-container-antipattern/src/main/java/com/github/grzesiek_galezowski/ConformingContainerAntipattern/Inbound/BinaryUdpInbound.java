package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core.ProcessingWorkflow;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;

public class BinaryUdpInbound implements Inbound {
  private final InputSocket _socket;
  private final PacketParsing _parsing;
  private ProcessingWorkflow _processingWorkflow;

  public BinaryUdpInbound() {
    _socket = ApplicationRoot.CONTEXT.resolve(InputSocket.class);
    _parsing = ApplicationRoot.CONTEXT.resolve(PacketParsing.class);
  }

  public void setDomainLogic(ProcessingWorkflow processingWorkflow) {
    _processingWorkflow = processingWorkflow;
  }

  public void startListening() {
    byte[] frameData = new byte[100];
    while (_socket.receive(frameData)) {
      InboundMessage message = _parsing.resultFor(frameData);
      if (message != null) {
        if (_processingWorkflow != null) {
          _processingWorkflow.applyTo(message);
        }
      }
    }
  }
}
