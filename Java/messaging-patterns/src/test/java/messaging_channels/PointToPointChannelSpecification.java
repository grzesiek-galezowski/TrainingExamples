package messaging_channels;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class PointToPointChannelSpecification {

  @Test
  public void example() throws InterruptedException {
    PointToPointChannel.Server server
        = new PointToPointChannel.Server(
            new PointToPointChannel.Client());

    server.start();

    Thread.sleep(10000);
  }


}