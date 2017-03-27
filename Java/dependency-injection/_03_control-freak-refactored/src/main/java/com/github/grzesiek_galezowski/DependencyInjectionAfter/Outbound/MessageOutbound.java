package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.IOutbound;

public class MessageOutbound implements IOutbound {
  private final ISocket _outputSocket;
  private final IOutboundMessageFactory _outboundMessageFactory;

  public MessageOutbound(
      ISocket outputSocket,
      IOutboundMessageFactory outboundMessageFactory) {
    _outputSocket = outputSocket;
    _outboundMessageFactory = outboundMessageFactory;
  }

  public void Send(AcmeMessage message) {
    IOutboundMessage outboundMessage = _outboundMessageFactory.CreateOutboundMessage();
    message.WriteTo(outboundMessage);
    outboundMessage.SendVia(_outputSocket);
  }
}
