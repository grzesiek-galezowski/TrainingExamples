package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;

public interface OutboundMessage extends DataDestination{
  void sendVia(Socket outputSocket);
}
