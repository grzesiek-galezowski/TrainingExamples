package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound;

public interface OutputSocket {
  void open();

  void close();

  void send(String lol);
}
