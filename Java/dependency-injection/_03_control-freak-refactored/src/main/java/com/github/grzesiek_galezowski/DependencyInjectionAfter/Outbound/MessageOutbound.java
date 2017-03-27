package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public class MessageOutbound implements Outbound {
  private final Socket _outputSocket;
  private final OutboundMessageFactory _outboundMessageFactory;

  public MessageOutbound(
      Socket outputSocket,
      OutboundMessageFactory outboundMessageFactory) {
    _outputSocket = outputSocket;
    _outboundMessageFactory = outboundMessageFactory;
  }

  public void Send(AcmeMessage message) {
    OutboundMessage outboundMessage = _outboundMessageFactory.CreateOutboundMessage();
    message.WriteTo(outboundMessage);
    outboundMessage.SendVia(_outputSocket);
  }
}
