package com.github.grzesiek_galezowski.BastardInjection.Outbound;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.DataDestination;

public interface IOutboundMessage extends DataDestination {
  void sendVia(IOutputSocket outputOutputSocket);
}
