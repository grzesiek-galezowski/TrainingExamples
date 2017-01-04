package ServiceLocatorAntipattern.Outbound;

import ServiceLocatorAntipattern.ApplicationRoot;

public class OutboundMessage implements IOutboundMessage {
  private final IMarshalling _marshalling;
  private String _content = "";

  public OutboundMessage() {
    _marshalling = ApplicationRoot.context.getComponent(IMarshalling.class);
  }

  public void sendVia(OutputSocket outputOutputSocket) {
    String marshalledContent = _marshalling.of(_content);
    outputOutputSocket.open();
    outputOutputSocket.send(marshalledContent);
    outputOutputSocket.close();
  }

  public void add(String content) {
    _content += content;
  }
}