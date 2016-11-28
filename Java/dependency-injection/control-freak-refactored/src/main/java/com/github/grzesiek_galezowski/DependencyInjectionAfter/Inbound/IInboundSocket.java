package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

public interface IInboundSocket {
  boolean Receive(byte[] frameData);
}
