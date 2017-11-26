package com.examples.application;

import com.examples.inbound.ports.InboundMessageHandler;
import com.examples.outbound.ports.OutputChannel;
import com.examples.storage.ports.MessageRepository;

public class ApplicationLogic implements InboundMessageHandler {

  private OutputChannel outputChannel;
  private MessageRepository messageRepository;

  public ApplicationLogic(
      final OutputChannel outputChannel,
      final MessageRepository messageRepository) {
    this.outputChannel = outputChannel;
    this.messageRepository = messageRepository;
  }

  @Override
  public void newMessage() {
    messageRepository.store();
    outputChannel.sendNotification();
  }
}
