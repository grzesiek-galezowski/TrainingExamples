package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;

public interface IOutboundMessage extends DataDestination{
  void SendVia(ISocket outputSocket);
}
