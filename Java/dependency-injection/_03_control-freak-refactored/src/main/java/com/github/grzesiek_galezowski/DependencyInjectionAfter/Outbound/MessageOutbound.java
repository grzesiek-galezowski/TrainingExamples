package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public class MessageOutbound implements Outbound {
  private final Socket outputSocket;
  private final OutboundMessageFactory outboundMessageFactory;

  public MessageOutbound(
      Socket outputSocket,
      OutboundMessageFactory outboundMessageFactory) {
    this.outputSocket = outputSocket;
    this.outboundMessageFactory = outboundMessageFactory;
  }

  public void send(AcmeMessage message) {
    OutboundMessage outboundMessage = outboundMessageFactory.createOutboundMessage();
    message.writeTo(outboundMessage);
    outboundMessage.sendVia(outputSocket);
  }
}
