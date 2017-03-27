package com.github.grzesiek_galezowski.BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.Outbound;

public interface ProcessingWorkflow
  {
    void setOutbound(Outbound outbound);
    void applyTo(Message message);
  }
