package com.github.grzesiek_galezowski.BastardInjection.Outbound;

class XmlOutboundMessage implements OutboundMessage {
  private final Marshalling _marshalling;
  private String _content = "";

  public XmlOutboundMessage() {
    this(new XmlMarshalling());
  }

  public XmlOutboundMessage(Marshalling marshalling) {
    _marshalling = marshalling;
  }

  public void sendVia(OutputSocket outputOutputSocket) {
    String marshalledContent = _marshalling.Of(_content);
    outputOutputSocket.open();
    outputOutputSocket.send(marshalledContent);
    outputOutputSocket.close();
  }

  public void add(String content) {
    _content += content;
  }

}