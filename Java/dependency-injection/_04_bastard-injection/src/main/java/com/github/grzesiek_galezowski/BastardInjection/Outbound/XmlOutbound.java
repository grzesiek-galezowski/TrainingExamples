package com.github.grzesiek_galezowski.BastardInjection.Outbound;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;

public class XmlOutbound implements Outbound {
  private final OutputSocket _outputSocket;

  public XmlOutbound() {
    this(new TcpSocket());
  }

  //for tests
  public XmlOutbound(OutputSocket outputSocket) {
    _outputSocket = outputSocket;
  }

  public void send(Message message) {
    XmlOutboundMessage outboundMessage = new XmlOutboundMessage();
    message.writeTo(outboundMessage);
    outboundMessage.sendVia(_outputSocket);
  }
}
