package com.github.grzesiek_galezowski.BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.IOutbound;

public interface IProcessingWorkflow
  {
    void setOutbound(IOutbound outbound);
    void applyTo(AcmeMessage message);
  }
