package com.github.grzesiek_galezowski.BastardInjection.Outbound;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.DataDestination;

public interface OutboundMessage extends DataDestination {
  void sendVia(OutputSocket outputOutputSocket);
}
