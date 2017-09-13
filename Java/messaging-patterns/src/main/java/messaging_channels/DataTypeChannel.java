package messaging_channels;

import com.fasterxml.jackson.databind.ObjectMapper;
import io.reactivex.Flowable;
import io.reactivex.schedulers.Schedulers;
import messaging_channels.events.ItemDelivered;
import messaging_channels.events.PurchaseMade;

import java.io.IOException;
import java.util.concurrent.TimeUnit;

import static java.lang.System.out;

public class DataTypeChannel {

  public static class Server {
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public Flowable<String> purchases() {
      return Flowable
          .interval(1, TimeUnit.SECONDS)
          .map(time -> objectMapper.writeValueAsString(new PurchaseMade()));
    }

    public Flowable<String> deliveries() {
      return Flowable
          .interval(1, TimeUnit.SECONDS)
          .map(time -> objectMapper.writeValueAsString(new ItemDelivered()));
    }


  }
  public static class Client {
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public void onPurchaseMade(final String payload) throws IOException {
      final PurchaseMade purchaseMade
          = objectMapper.readValue(payload, PurchaseMade.class);
      out.println(purchaseMade);
    }

    public void onItemDelivered(final String payload) throws IOException {
      final ItemDelivered itemDelivered
          = objectMapper.readValue(payload, ItemDelivered.class);
      out.println(itemDelivered);
    }

  }
}
