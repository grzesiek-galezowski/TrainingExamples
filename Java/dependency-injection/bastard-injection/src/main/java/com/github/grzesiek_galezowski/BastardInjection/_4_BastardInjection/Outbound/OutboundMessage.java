package com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Outbound;

import com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Interfaces.DataDestination;

public interface IOutboundMessage extends DataDestination {
  void SendVia(IOutputSocket outputOutputSocket);
}

  class OutboundMessage implements IOutboundMessage {
    private final IMarshalling _marshalling;
    private String _content = "";

    public OutboundMessage() {
      this(new XmlMarshalling());
    }

    public OutboundMessage(IMarshalling marshalling) {
      _marshalling = marshalling;
    }

    public void SendVia(IOutputSocket outputOutputSocket) {
      var marshalledContent = _marshalling.Of(_content);
      outputOutputSocket.Open();
      outputOutputSocket.Send(marshalledContent);
      outputOutputSocket.Close();
    }

    public void Add(String s) {
      _content += s;
    }

  }