package processing;

import channels.OutputChannel;
import handlers.InboundMessageHandler;
import repositories.MessageRepository;

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
