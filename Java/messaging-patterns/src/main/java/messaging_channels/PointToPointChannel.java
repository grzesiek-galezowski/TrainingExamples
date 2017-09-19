package messaging_channels;

import com.fasterxml.jackson.databind.ObjectMapper;
import io.reactivex.Flowable;
import messaging_channels.events.ItemDelivered;
import messaging_channels.events.PurchaseMade;

import java.io.IOException;
import java.util.concurrent.TimeUnit;

import static java.lang.System.out;

public class PointToPointChannel {
  public static class Server {

    private static final ObjectMapper objectMapper = new ObjectMapper();
    private Client client;

    public Server(Client client) {
      this.client = client;
    }

    public void start() {
      Flowable
          .interval(1, TimeUnit.SECONDS)
          .map(time -> objectMapper.writeValueAsString(new PurchaseMade()))
          .forEach(str -> client.onPurchaseMade(str));
    }

  }

  public static class Client {
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public void onPurchaseMade(final String payload) throws IOException {
      final PurchaseMade purchaseMade
          = objectMapper.readValue(payload, PurchaseMade.class);
      out.println(purchaseMade);
    }

  }
}
