package messaging_channels;

import de.scravy.either.Either;
import io.reactivex.flowables.GroupedFlowable;
import io.reactivex.functions.Consumer;

public class Splitter {

  public <T, U> void split(
      final GroupedFlowable<Either<T, U>, Either<T, U>> e,
      final Consumer<T> onPurchaseMade,
      final Consumer<U> onInvalidMessage) {

    if (e.getKey().isLeft()) {
      e.map(v -> v.getLeft()).subscribe(onPurchaseMade);
    } else {
      e.map(v -> v.getRight()).subscribe(onInvalidMessage);
    }
  }
}