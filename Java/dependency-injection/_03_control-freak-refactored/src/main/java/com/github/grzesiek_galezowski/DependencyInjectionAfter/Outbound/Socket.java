package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public interface Socket {
  void open();

  void close();

  void send(String lol);
}