package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Outbound;

public class TcpSocket {
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
