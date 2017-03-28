package com.github.grzesiek_galezowski.BastardInjection.Outbound;

class XmlOutboundMessage implements OutboundMessage {
  private final Marshalling marshalling;
  private String content = "";

  public XmlOutboundMessage() {
    this(new XmlMarshalling());
  }

  public XmlOutboundMessage(Marshalling marshalling) {
    this.marshalling = marshalling;
  }

  public void sendVia(OutputSocket outputOutputSocket) {
    String marshalledContent = marshalling.Of(content);
    outputOutputSocket.open();
    outputOutputSocket.send(marshalledContent);
    outputOutputSocket.close();
  }

  public void add(String content) {
    this.content += content;
  }

}