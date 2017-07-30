package controllers;

import handlers.InboundMessageHandler;

public class RestController {
  private InboundMessageHandler messageHandler;

  public RestController(final InboundMessageHandler messageHandler) {

    this.messageHandler = messageHandler;
  }

  public void put() {
    messageHandler.newMessage();
  }

}
