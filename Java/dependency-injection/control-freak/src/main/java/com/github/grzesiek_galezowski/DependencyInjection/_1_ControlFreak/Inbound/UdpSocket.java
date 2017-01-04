package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Inbound;

import java.util.Random;

class UdpSocket {
  public boolean receive(byte[] frameData) {
    new Random().nextBytes(frameData);
    return true;
  }
}
