package repositories;

import static java.lang.System.out;

public class RedisRepository implements MessageRepository {
  @Override
  public void store() {
    out.println("stored");
  }
}
