package com.github.TrainingExamples.grzesiek_galezowski;

import com.github.TrainingExamples.grzesiek_galezowski.commands.SessionEnd;
import com.github.TrainingExamples.grzesiek_galezowski.commands.SessionInit;
import com.github.TrainingExamples.grzesiek_galezowski.commands.SessionPayload;

public class V2ProtocolMessageFactory implements MessageFactory {
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
