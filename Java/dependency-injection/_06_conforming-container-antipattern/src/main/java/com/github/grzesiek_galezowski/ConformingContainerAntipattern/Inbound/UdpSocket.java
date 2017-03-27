package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;

import java.util.Random;

public class UdpSocket implements InputSocket {
  public boolean receive(byte[] frameData) {
    ApplicationRoot.CONTEXT.resolve(Random.class).nextBytes(frameData); //stable dependency!
    return true;
  }
}
