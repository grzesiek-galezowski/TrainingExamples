package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Outbound;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.AcmeMessage;

public class XmlTcpOutbound {
  private final TcpSocket outputSocket;

  public XmlTcpOutbound() {
    outputSocket = new TcpSocket();
  }

  public void send(AcmeMessage message) {
    XmlOutboundMessage outboundMessage = new XmlOutboundMessage();
    message.writeTo(outboundMessage);
    outboundMessage.sendVia(outputSocket);
  }
}
