package ServiceLocatorAntipattern.Inbound;

import ServiceLocatorAntipattern.ApplicationRoot;

import java.util.Random;

public class UdpSocket implements IInputSocket {
  public boolean receive(byte[] frameData) {
    ApplicationRoot.context.getComponent(Random.class).nextBytes(frameData); //stable dependency!
    return true;
  }
}