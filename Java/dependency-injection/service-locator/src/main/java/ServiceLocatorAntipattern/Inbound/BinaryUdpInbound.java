package ServiceLocatorAntipattern.Inbound;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Core.ProcessingWorkflow;
import ServiceLocatorAntipattern.Interfaces.AcmeMessage;

public class BinaryUdpInbound implements IInbound {
  private final IInputSocket _socket;
  private final PacketParsing _parsing;
  private ProcessingWorkflow _processingWorkflow;

  public BinaryUdpInbound() {
    _socket = ApplicationRoot.context.getComponent(IInputSocket.class);
    _parsing = ApplicationRoot.context.getComponent(PacketParsing.class);
  }

  public void setDomainLogic(ProcessingWorkflow processingWorkflow) {
    _processingWorkflow = processingWorkflow;
  }

  public void startListening() { //todo look at earlier examples
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