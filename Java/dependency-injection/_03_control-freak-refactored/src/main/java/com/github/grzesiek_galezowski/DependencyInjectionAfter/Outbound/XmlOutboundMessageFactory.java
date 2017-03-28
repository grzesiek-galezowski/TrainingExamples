package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public class XmlOutboundMessageFactory implements OutboundMessageFactory {
  public OutboundMessage createOutboundMessage() {
    return new XmlOutboundMessage(new XmlMarshalling());
  }
}
