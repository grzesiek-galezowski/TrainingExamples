package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Outbound;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.DataDestination;

public class XmlOutboundMessage implements DataDestination {
  private final XmlMarshalling _xmlMarshalling;
  private String _content = "";

  public XmlOutboundMessage() {
    _xmlMarshalling = new XmlMarshalling();
  }

  public void sendVia(TcpSocket outputSocket) {
    String marshalledContent = _xmlMarshalling.of(_content);
    outputSocket.open();
    outputSocket.send(marshalledContent);
    outputSocket.close();
  }

  public void add(String s) {
    _content += s;
  }
}
