package messaging_channels;

import com.fasterxml.jackson.databind.ObjectMapper;
import io.reactivex.Flowable;
import messaging_channels.events.PurchaseMade;

import java.util.concurrent.TimeUnit;

public class MessagingBridge {
  public static class LeftParticipant {
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public Flowable<String> purchasesChannel() {
      return Flowable
          .interval(1, TimeUnit.SECONDS)
          .map(time -> objectMapper.writeValueAsString(new PurchaseMade()))
          .cache();
    }

    public void onPurchase(String purchaseJson) {

    }

  }

  public static class LeftClient {
    private static final ObjectMapper objectMapper = new ObjectMapper();

  }

  public static class RightServer {
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public Flowable<String> purchasesChannel() {
      return Flowable
          .interval(1, TimeUnit.SECONDS)
          .map(time -> objectMapper.writeValueAsString(new PurchaseMade()));
    }
  }

  public static class RightClient {
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public void onPurchase(String purchaseJson) {

    }
  }
}
