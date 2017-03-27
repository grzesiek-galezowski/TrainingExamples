package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound;

public interface InputSocket {
  boolean receive(byte[] frameData);
}
