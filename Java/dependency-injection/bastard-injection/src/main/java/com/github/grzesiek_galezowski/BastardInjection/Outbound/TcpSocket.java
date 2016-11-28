package com.github.grzesiek_galezowski.BastardInjection.Outbound;

class TcpSocket implements IOutputSocket {
  public void open() {
    System.out.println("open");
  }
  public void close() {
    System.out.println("closing");
  }
  public void send(String lol) {
    System.out.println(lol);
  }
}
