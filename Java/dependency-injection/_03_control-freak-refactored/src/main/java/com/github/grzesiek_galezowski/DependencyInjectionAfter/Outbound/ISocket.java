package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public interface ISocket {
  void Open();

  void Close();

  void Send(String lol);
}