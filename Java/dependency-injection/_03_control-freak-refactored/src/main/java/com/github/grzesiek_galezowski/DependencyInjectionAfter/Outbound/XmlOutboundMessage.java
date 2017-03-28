package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;

public class XmlOutboundMessage implements DataDestination, OutboundMessage {
  private final XmlMarshalling xmlMarshalling;
  private String _content = "";

  public XmlOutboundMessage(XmlMarshalling xmlMarshalling) {
    this.xmlMarshalling = xmlMarshalling;
  }

  public void SendVia(Socket outputSocket) {
    String marshalledContent = xmlMarshalling.Of(_content);
    outputSocket.Open();
    outputSocket.Send(marshalledContent);
    outputSocket.Close();
  }

  public void Add(String s) {
    _content += s;
  }
}
