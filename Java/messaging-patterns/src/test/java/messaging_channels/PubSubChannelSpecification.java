package messaging_channels;

import org.junit.jupiter.api.Test;

class PubSubChannelSpecification {
  @Test
  public void example() throws InterruptedException {
    PubSubChannel.Client client = new PubSubChannel.Client();
    PubSubChannel.Server server = new PubSubChannel.Server();

    //pub-sub channel is chosen, but this is not important
    server.purchasesChannel().subscribe(client::onPurchaseMade);
    Thread.sleep(20000);
  }
}