package messaging_channels;

import com.fasterxml.jackson.databind.ObjectMapper;
import de.scravy.either.Either;
import io.reactivex.BackpressureStrategy;
import io.reactivex.Flowable;
import io.reactivex.Maybe;
import messaging_channels.events.PurchaseMade;
import org.reactivestreams.Subscriber;
import org.reactivestreams.Subscription;

import java.io.IOException;
import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.List;
import java.util.Queue;
import java.util.concurrent.TimeUnit;
import java.util.function.Function;

import static java.lang.System.out;

public class InvalidMessageChannel {

  public static class Server {
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public Flowable<String> purchasesChannel() {
      return Flowable
          .interval(1, TimeUnit.SECONDS)
          .map(time -> {
            if( time % 3 == 0) {
              return objectMapper.writeValueAsString(new PurchaseMade());
            } else {
              return "lolokimono";
            }
          });
    }
  }

  public static class Deserializer {
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public Either<PurchaseMade, String> deserialize(final String payload) {
      try {
        final PurchaseMade purchaseMade
            = objectMapper.readValue(payload, PurchaseMade.class);
        return Either.left(purchaseMade);
      } catch (Throwable e) {
        return Either.right(payload);
      }
    }
  }

  public static class PurchasesClient {
    public void onPurchaseMade(final PurchaseMade purchaseMade) {
      out.println("PURCHASE: " + purchaseMade);
    }
  }

  public static class InvalidMessagesClient {
    public void onInvalidMessage(final String invalidMessage) {
      out.println("INVALID: " + invalidMessage);
    }
  }

}
