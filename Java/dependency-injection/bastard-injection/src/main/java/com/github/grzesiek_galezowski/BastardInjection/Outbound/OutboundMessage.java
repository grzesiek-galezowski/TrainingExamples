package com.github.grzesiek_galezowski.BastardInjection.Outbound;

class OutboundMessage implements IOutboundMessage {
  private final IMarshalling _marshalling;
  private String _content = "";

  public OutboundMessage() {
    this(new XmlMarshalling());
  }

  public OutboundMessage(IMarshalling marshalling) {
    _marshalling = marshalling;
  }

  public void sendVia(IOutputSocket outputOutputSocket) {
    String marshalledContent = _marshalling.Of(_content);
    outputOutputSocket.open();
    outputOutputSocket.send(marshalledContent);
    outputOutputSocket.close();
  }

  public void add(String s) {
    _content += s;
  }

}