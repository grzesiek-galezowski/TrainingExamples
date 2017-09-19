package messaging_channels;

import org.junit.jupiter.api.Test;

import static messaging_channels.InvalidMessageChannel.*;

class InvalidMessageChannelSpecification {

  @Test
  public void example() throws InterruptedException {
    Server server = new Server();
    Deserializer deserializer = new Deserializer();
    Splitter splitter = new Splitter();
    PurchasesClient purchasesClient = new PurchasesClient();
    InvalidMessagesClient invalidMessagesClient = new InvalidMessagesClient();

    server.purchasesChannel()
        .map(deserializer::deserialize)
        .groupBy(e -> e)
        .subscribe(e -> splitter.split(e,
            purchasesClient::onPurchaseMade,
            invalidMessagesClient::onInvalidMessage));

    Thread.sleep(10000);
  }



}