package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;

public class XmlOutboundMessage implements DataDestination, OutboundMessage {
  private final XmlMarshalling _xmlMarshalling;
  private String _content = "";

  public XmlOutboundMessage(XmlMarshalling xmlMarshalling) {
    _xmlMarshalling = xmlMarshalling;
  }

  public void SendVia(Socket outputSocket) {
    String marshalledContent = _xmlMarshalling.Of(_content);
    outputSocket.Open();
    outputSocket.Send(marshalledContent);
    outputSocket.Close();
  }

  public void Add(String s) {
    _content += s;
  }
}
