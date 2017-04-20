package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public interface Parsing {
  AcmeMessage resultFor(byte[] frameData);
}
