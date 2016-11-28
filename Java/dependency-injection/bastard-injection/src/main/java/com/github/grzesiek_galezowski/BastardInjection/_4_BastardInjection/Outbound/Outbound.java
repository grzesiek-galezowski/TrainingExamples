package com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Outbound;

import com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Interfaces.AcmeMessage;

interface IOutbound {
  void Send(AcmeMessage message);
}

  public class Outbound implements IOutbound {
    private final IOutputSocket _outputSocket;

    public Outbound() {
      this(new TcpSocket());
    }

    //for tests
    public Outbound(IOutputSocket outputSocket) {
      _outputSocket = outputSocket;
    }

    public void Send(AcmeMessage message) {
      OutboundMessage outboundMessage = new OutboundMessage();
      message.WriteTo(outboundMessage);
      outboundMessage.SendVia(_outputSocket);
    }
  }
