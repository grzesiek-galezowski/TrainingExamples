package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public class XmlOutboundMessageFactory implements OutboundMessageFactory {
  public OutboundMessage CreateOutboundMessage() {
    return new XmlOutboundMessage(new XmlMarshalling());
  }
}
