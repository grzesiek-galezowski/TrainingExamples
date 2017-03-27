package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

public interface InboundSocket {
  boolean Receive(byte[] frameData);
}
