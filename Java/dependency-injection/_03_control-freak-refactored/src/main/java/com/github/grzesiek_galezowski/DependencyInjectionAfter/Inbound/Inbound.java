package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.ProcessingWorkflow;

public interface Inbound {
  void setDomainLogic(ProcessingWorkflow processingWorkflow);

  void startListening();
}
