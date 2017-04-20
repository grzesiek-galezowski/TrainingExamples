package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;

public class XmlOutboundMessage implements OutboundMessage {
  private final Marshalling marshalling;
  private String content = "";

  public XmlOutboundMessage() {
    marshalling = ApplicationRoot.CONTEXT.resolve(Marshalling.class);
  }

  public void sendVia(OutputSocket outputOutputSocket) {
    String marshalledContent = marshalling.of(content);
    outputOutputSocket.open();
    outputOutputSocket.send(marshalledContent);
    outputOutputSocket.close();
  }

  public void add(String content) {
    this.content += content;
  }
}
