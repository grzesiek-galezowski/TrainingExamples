package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core.ProcessingWorkflow;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;

public class BinaryUdpInbound implements Inbound {
  private final InputSocket socket;
  private final PacketParsing parsing;
  private ProcessingWorkflow processingWorkflow;

  public BinaryUdpInbound() {
    socket = ApplicationRoot.CONTEXT.resolve(InputSocket.class);
    parsing = ApplicationRoot.CONTEXT.resolve(PacketParsing.class);
  }

  public void setDomainLogic(ProcessingWorkflow processingWorkflow) {
    this.processingWorkflow = processingWorkflow;
  }

  public void startListening() {
    byte[] frameData = new byte[100];
    while (socket.receive(frameData)) {
      InboundMessage message = parsing.resultFor(frameData);
      if (message != null) {
        if (processingWorkflow != null) {
          processingWorkflow.applyTo(message);
        }
      }
    }
  }
}
