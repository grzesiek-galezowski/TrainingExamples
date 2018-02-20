package com.github.TrainingExamples.grzesiek_galezowski;

import com.github.TrainingExamples.grzesiek_galezowski.Commands.SessionEnd;
import com.github.TrainingExamples.grzesiek_galezowski.Commands.SessionInit;
import com.github.TrainingExamples.grzesiek_galezowski.Commands.SessionPayload;

@SuppressWarnings("unused")
public class Version2ProtocolMessageFactory implements MessageFactory {
  @Override
  public Message from(MessageDto rawData) {
    //todo new

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
