package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound.Outbound;

public interface ProcessingWorkflow {
  void setOutbound(Outbound outbound);

  void applyTo(InboundMessage message);
}
