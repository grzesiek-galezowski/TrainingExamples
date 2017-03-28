package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import java.util.Random;

public class UdpSocket implements InboundSocket {

  private final Random random;

  public UdpSocket() {
    random = new Random();
  }

  public boolean receive(byte[]frameData) {
    frameData = new byte[100];
    random.nextBytes(frameData);
    return true;
  }
}
