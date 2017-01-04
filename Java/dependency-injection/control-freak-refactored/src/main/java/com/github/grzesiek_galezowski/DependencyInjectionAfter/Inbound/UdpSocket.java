package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import java.util.Random;

public class UdpSocket implements IInboundSocket {
  public boolean Receive(byte[]frameData) {
    frameData = new byte[100];
    new Random().nextBytes(frameData);
    return true;
  }
}
