package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.DataDestination;

public interface OutboundMessage extends DataDestination {
  void sendVia(OutputSocket outputOutputSocket);
}
