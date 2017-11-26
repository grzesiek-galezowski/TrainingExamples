package com.examples.outbound.adapters;

import com.examples.outbound.ports.OutputChannel;

public class KafkaChannel implements OutputChannel {

  @Override
  public void sendNotification() {
    System.out.println("lol");
  }
}
