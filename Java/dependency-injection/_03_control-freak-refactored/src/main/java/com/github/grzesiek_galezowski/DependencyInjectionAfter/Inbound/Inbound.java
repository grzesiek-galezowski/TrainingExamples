package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.ProcessingWorkflow;

public interface Inbound {
  void SetDomainLogic(ProcessingWorkflow processingWorkflow);

  void StartListening();
}
