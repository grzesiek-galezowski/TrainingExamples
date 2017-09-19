package messaging_channels;

import com.fasterxml.jackson.databind.ObjectMapper;
import io.reactivex.Flowable;
import messaging_channels.events.ItemDelivered;
import messaging_channels.events.PurchaseMade;

import java.io.IOException;
import java.util.concurrent.TimeUnit;

import static java.lang.System.out;

public class PubSubChannel {

  public static class Server {
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public Flowable<String> purchasesChannel() {
      return Flowable
          .interval(1, TimeUnit.SECONDS)
          .map(time -> objectMapper.writeValueAsString(new PurchaseMade()));
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
