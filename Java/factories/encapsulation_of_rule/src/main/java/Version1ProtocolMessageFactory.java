public class Version1ProtocolMessageFactory implements MessageFactory {
  @Override
  public Message from(MessageDto rawData) {
    if(rawData.isSessionInit()) {
      return new SessionInit(rawData);
    } else if(rawData.isSessionEnd()) {
      return new SessionEnd(rawData);
    } else if(rawData.isSessionPayload()) {
      return new SessionPayload(rawData);
    } else {
      throw new UnknownMessageException(rawData);
    }
  }
}
