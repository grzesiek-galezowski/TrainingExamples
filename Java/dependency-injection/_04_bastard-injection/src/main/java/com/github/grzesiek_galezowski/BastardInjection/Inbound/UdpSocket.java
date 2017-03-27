package com.github.grzesiek_galezowski.BastardInjection.Inbound;

class UdpSocket implements InputSocket {
  public boolean receive(byte[] frameData) {
    //todo generate random the java way new Random().NextBytes(frameData);
    return true;
  }

  public void close() {
    System.out.println("Disposed");
  }
}
