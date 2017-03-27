package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Inbound;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core.ProcessingWorkflow;

public interface Inbound {
  void setDomainLogic(ProcessingWorkflow processingWorkflow);

  void startListening();
}
