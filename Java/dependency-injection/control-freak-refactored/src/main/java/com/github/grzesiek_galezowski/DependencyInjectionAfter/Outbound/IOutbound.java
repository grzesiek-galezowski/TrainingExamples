package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;

public interface IOutbound {
  void Send(AcmeMessage message);
}
