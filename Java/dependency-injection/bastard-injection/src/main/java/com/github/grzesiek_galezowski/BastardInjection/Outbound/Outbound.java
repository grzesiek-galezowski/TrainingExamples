package com.github.grzesiek_galezowski.BastardInjection.Outbound;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;

public class Outbound implements IOutbound {
  private final IOutputSocket _outputSocket;

  public Outbound() {
    this(new TcpSocket());
  }

  //for tests
  public Outbound(IOutputSocket outputSocket) {
    _outputSocket = outputSocket;
  }

  public void send(AcmeMessage message) {
    OutboundMessage outboundMessage = new OutboundMessage();
    message.writeTo(outboundMessage);
    outboundMessage.sendVia(_outputSocket);
  }
}
