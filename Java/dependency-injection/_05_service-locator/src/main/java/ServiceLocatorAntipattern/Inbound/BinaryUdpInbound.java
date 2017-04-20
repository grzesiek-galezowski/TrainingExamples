package ServiceLocatorAntipattern.Inbound;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Core.ProcessingWorkflow;
import ServiceLocatorAntipattern.Interfaces.Message;

public class BinaryUdpInbound implements Inbound {
  private final InputSocket _socket;
  private final PacketParsing _parsing;
  private ProcessingWorkflow processingWorkflow;

  public BinaryUdpInbound() {
    _socket = ApplicationRoot.context.getComponent(InputSocket.class);
    _parsing = ApplicationRoot.context.getComponent(PacketParsing.class);
  }

  public void setDomainLogic(ProcessingWorkflow processingWorkflow) {
    this.processingWorkflow = processingWorkflow;
  }

  public void startListening() { //todo look at earlier examples
    byte[] frameData = new byte[100];
    while (_socket.receive(frameData)) {
      Message message = _parsing.resultFor(frameData);
      if (message != null) {
        if (processingWorkflow != null) {
          processingWorkflow.applyTo(message);
        }
      }
    }
  }
}