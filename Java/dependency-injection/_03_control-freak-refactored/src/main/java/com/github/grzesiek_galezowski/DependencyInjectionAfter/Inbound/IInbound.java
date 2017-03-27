package com.github.grzesiek_galezowski.DependencyInjectionAfter.Inbound;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Core.IAcmeProcessingWorkflow;

public interface IInbound {
  void SetDomainLogic(IAcmeProcessingWorkflow processingWorkflow);

  void StartListening();
}
