package com.examples.inbound.adapters;

import com.examples.inbound.ports.InboundMessageHandler;

public class RestController {
  private InboundMessageHandler messageHandler;

  public RestController(final InboundMessageHandler messageHandler) {

    this.messageHandler = messageHandler;

  }

  public void put() {
    messageHandler.newMessage();
  }

}
