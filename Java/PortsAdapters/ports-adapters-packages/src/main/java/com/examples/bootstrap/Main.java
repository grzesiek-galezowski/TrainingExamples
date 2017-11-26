package com.examples.bootstrap;

import com.examples.application.ApplicationLogic;
import com.examples.inbound.adapters.RestController;
import com.examples.outbound.adapters.KafkaChannel;
import com.examples.storage.adapters.RedisRepository;

public class Main {
  public void main(String[] args) {
    new RestController(new ApplicationLogic(
        new KafkaChannel(),
        new RedisRepository()));
  }
}
