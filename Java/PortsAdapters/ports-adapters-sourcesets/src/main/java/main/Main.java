package main;import channels.KafkaChannel;
import controllers.RestController;
import processing.ApplicationLogic;
import repositories.RedisRepository;

public class Main {
  public void main(String[] args) {
    new RestController(new ApplicationLogic(
        new KafkaChannel(),
        new RedisRepository()));
  }
}
