package com.github.TrainingExamples.grzesiek_galezowski;

import com.github.TrainingExamples.grzesiek_galezowski.commands.SessionEnd;
import com.github.TrainingExamples.grzesiek_galezowski.commands.SessionInit;
import com.github.TrainingExamples.grzesiek_galezowski.commands.SessionPayload;

@SuppressWarnings("unused")
public class V1MessageFactory implements MessageFactory {
  @Override
  public Message from(MessageDto rawData) {
    //todo new kind of message
    //todo replace everything with null object

    if (rawData.isSessionInit) {
      return new SessionInit(rawData);
    } else if (rawData.isSessionEnd) {
      return new SessionEnd(rawData);
    } else if (rawData.isSessionPayload) {
      return new SessionPayload(rawData);
    } else {
      throw new UnknownMessageException(rawData);
    }
  }
}
