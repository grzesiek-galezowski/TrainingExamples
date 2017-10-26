public class MessageDto {
  public MessageTypes MessageType;
  private boolean sessionInit;
  private boolean sessionEnd;
  private boolean sessionPayload;

  public boolean isSessionInit() {
    return sessionInit;
  }

  public boolean isSessionEnd() {
    return sessionEnd;
  }

  public boolean isSessionPayload() {
    return sessionPayload;
  }
}
