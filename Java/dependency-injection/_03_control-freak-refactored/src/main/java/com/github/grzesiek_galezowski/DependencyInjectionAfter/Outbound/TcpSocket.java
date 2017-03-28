package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public class TcpSocket implements Socket {
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
