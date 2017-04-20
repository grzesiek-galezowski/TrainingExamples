package ServiceLocatorAntipattern.Outbound;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Interfaces.Message;

public class MessageOutbound implements Outbound {
  private final OutputSocket outputOutputSocket;

  public MessageOutbound() {
    outputOutputSocket = ApplicationRoot.context.getComponent(OutputSocket.class);
  }

  public void send(Message message) {
    OutboundMessage outboundMessage = ApplicationRoot.context.getComponent(OutboundMessage.class);
    message.writeTo(outboundMessage);
    outboundMessage.sendVia(outputOutputSocket);
  }
}
