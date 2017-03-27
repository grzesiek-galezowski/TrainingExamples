package com.github.grzesiek_galezowski.DependencyInjectionAfter.Core;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.Outbound;

public interface ProcessingWorkflow {
  void SetOutbound(Outbound outbound);

  void ApplyTo(AcmeMessage message);
}
