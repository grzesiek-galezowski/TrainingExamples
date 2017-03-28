package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;

public class XmlOutboundMessage implements DataDestination, OutboundMessage {
  private final XmlMarshalling xmlMarshalling;
  private String _content = "";

  public XmlOutboundMessage(XmlMarshalling xmlMarshalling) {
    this.xmlMarshalling = xmlMarshalling;
  }

  public void sendVia(Socket outputSocket) {
    String marshalledContent = xmlMarshalling.of(_content);
    outputSocket.open();
    outputSocket.send(marshalledContent);
    outputSocket.close();
  }

  public void add(String s) {
    _content += s;
  }
}
