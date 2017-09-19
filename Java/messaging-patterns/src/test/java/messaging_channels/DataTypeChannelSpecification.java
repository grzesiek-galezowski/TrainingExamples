package messaging_channels;

import org.junit.jupiter.api.Test;

class DataTypeChannelSpecification {
  @Test
  public void lol() throws InterruptedException {
    DataTypeChannel.Client client = new DataTypeChannel.Client();
    DataTypeChannel.Server server = new DataTypeChannel.Server();

    //pub-sub channel is chosen, but this is not important
    server.purchasesChannel().subscribe(client::onPurchaseMade);
    server.deliveriesChannel().subscribe(client::onItemDelivered);
    Thread.sleep(20000);
  }
}