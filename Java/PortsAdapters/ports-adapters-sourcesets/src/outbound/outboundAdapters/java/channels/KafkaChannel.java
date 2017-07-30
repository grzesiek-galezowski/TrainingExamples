package channels;

public class KafkaChannel implements OutputChannel {

  @Override
  public void sendNotification() {
    System.out.println("lol");
  }
}
