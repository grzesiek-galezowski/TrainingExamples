package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public interface Socket {
  void Open();

  void Close();

  void Send(String lol);
}