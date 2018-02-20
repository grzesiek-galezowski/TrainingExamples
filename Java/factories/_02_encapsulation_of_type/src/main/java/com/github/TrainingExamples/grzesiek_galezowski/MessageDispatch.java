package com.github.TrainingExamples.grzesiek_galezowski;

public class MessageDispatch {
  private final MessageFactory messageFactory;

  public MessageDispatch(final MessageFactory messageFactory) {
    this.messageFactory = messageFactory;
  }


  //todo before show new List() vs new ArrayList()
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

