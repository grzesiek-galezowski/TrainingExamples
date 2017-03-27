package com.github.grzesiek_galezowski.DependencyInjectionAfter.Core;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.IOutbound;

public interface IAcmeProcessingWorkflow {
  void SetOutbound(IOutbound outbound);

  void ApplyTo(AcmeMessage message);
}
