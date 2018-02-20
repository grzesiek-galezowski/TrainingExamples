package com.github.TrainingExamples.grzesiek_galezowski;

import com.github.TrainingExamples.grzesiek_galezowski.commands.SessionEnd;
import com.github.TrainingExamples.grzesiek_galezowski.commands.SessionInit;
import com.github.TrainingExamples.grzesiek_galezowski.commands.SessionPayload;

public class V1ProtocolMessageFactory implements MessageFactory {
  @Override
  public Message from(MessageDto rawData) {
    if(rawData.isSessionInit) {
      return new SessionInit(rawData);
    } else if(rawData.isSessionEnd) {
      return new SessionEnd(rawData);
    } else if(rawData.isSessionPayload) {
      return new SessionPayload(rawData);
    } else {
      throw new UnknownMessageException(rawData);
    }
  }
}
