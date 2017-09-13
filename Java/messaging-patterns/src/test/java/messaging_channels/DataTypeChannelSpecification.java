package messaging_channels;

import messaging_channels.events.PurchaseMade;
import org.junit.jupiter.api.Test;

class DataTypeChannelSpecification {
  @Test
  public void lol() throws InterruptedException {
    DataTypeChannel.Client client = new DataTypeChannel.Client();
    DataTypeChannel.Server server = new DataTypeChannel.Server();

    server.purchases().subscribe(client::onPurchaseMade);
    server.deliveries().subscribe(client::onItemDelivered);
    Thread.sleep(20000);
  }
}