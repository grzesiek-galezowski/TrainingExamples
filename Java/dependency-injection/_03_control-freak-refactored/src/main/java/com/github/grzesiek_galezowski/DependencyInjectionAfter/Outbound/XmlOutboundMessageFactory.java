package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public class XmlOutboundMessageFactory implements IOutboundMessageFactory {
  public IOutboundMessage CreateOutboundMessage() {
    return new XmlOutboundMessage(new XmlMarshalling());
  }
}
