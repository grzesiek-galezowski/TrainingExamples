package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public class TcpSocket implements Socket {
  public void Open() {
    System.out.println("open");
  }

  public void Close() {
    System.out.println("closing");
  }

  public void Send(String lol) {
    System.out.println(lol);
  }
}
