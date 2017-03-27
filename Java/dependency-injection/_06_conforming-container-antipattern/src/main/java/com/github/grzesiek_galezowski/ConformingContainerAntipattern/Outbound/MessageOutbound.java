package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;

public class MessageOutbound implements Outbound {
  private final OutputSocket _outputOutputSocket;

  public MessageOutbound() {
    _outputOutputSocket = ApplicationRoot.CONTEXT.resolve(OutputSocket.class);
  }

  public void send(InboundMessage message) {
    OutboundMessage outboundMessage = ApplicationRoot.CONTEXT.resolve(OutboundMessage.class);
    message.writeTo(outboundMessage);
    outboundMessage.sendVia(_outputOutputSocket);
  }
}
