package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Outbound;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.DataDestination;

public class XmlOutboundMessage implements DataDestination {
  private final XmlMarshalling _xmlMarshalling;
  private String content = "";

  public XmlOutboundMessage() {
    _xmlMarshalling = new XmlMarshalling();
  }

  public void sendVia(TcpSocket outputSocket) {
    String marshalledContent = _xmlMarshalling.of(content);
    outputSocket.open();
    outputSocket.send(marshalledContent);
    outputSocket.close();
  }

  public void add(String s) {
    content += s;
  }
}
