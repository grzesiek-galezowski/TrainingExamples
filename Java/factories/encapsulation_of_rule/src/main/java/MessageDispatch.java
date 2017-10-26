public class MessageDispatch {
  private final MessageFactory messageFactory;

  public MessageDispatch(final MessageFactory messageFactory) {
    this.messageFactory = messageFactory;
  }

  public void applyTo(MessageDto dto) {
    final Message msg = messageFactory.from(dto);
    try {
      msg.validate();
      msg.authorize();
      msg.handle();
    } finally {
      msg.respond();
    }
  }

}

