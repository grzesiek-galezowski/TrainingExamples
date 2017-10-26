package com.github.TrainingExamples.grzesiek_galezowski;

@SuppressWarnings("unused")
public class Version2ProtocolMessageFactory implements MessageFactory {
  @Override
  public Message from(MessageDto rawData) {
    switch (rawData.MessageType) {
      case SessionInit:
        return new SessionInit(rawData);
      case SessionEnd:
        return new SessionEnd(rawData);
      case SessionPayload:
        return new SessionPayload(rawData);
      default:
        throw new UnknownMessageException(rawData);
    }
  }
}
