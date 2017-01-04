package ServiceLocatorAntipattern.Outbound;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Interfaces.AcmeMessage;

public class MessageOutbound implements Outbound {
  private final OutputSocket _outputOutputSocket;

  public MessageOutbound() {
    _outputOutputSocket = ApplicationRoot.context.getComponent(OutputSocket.class);
  }

  public void send(AcmeMessage message) {
    IOutboundMessage outboundMessage = ApplicationRoot.context.getComponent(IOutboundMessage.class);
    message.writeTo(outboundMessage);
    outboundMessage.sendVia(_outputOutputSocket);
  }
}
