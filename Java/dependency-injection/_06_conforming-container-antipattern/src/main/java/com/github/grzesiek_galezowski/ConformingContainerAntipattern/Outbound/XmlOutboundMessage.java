package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;

public class XmlOutboundMessage implements OutboundMessage {
  private final Marshalling _marshalling;
  private String _content = "";

  public XmlOutboundMessage() {
    _marshalling = ApplicationRoot.CONTEXT.resolve(Marshalling.class);
  }

  public void sendVia(OutputSocket outputOutputSocket) {
    String marshalledContent = _marshalling.of(_content);
    outputOutputSocket.open();
    outputOutputSocket.send(marshalledContent);
    outputOutputSocket.close();
  }

  public void add(String content) {
    _content += content;
  }
}
