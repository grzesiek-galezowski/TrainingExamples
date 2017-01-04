package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound.Outbound;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services.Authorization;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services.Repository;

public class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final Repository _repository;
  private final Authorization _authorizationRules;
  private Outbound _outbound;

  public AcmeProcessingWorkflow() {
    _authorizationRules = ApplicationRoot.CONTEXT.resolve(Authorization.class);
    _repository = ApplicationRoot.CONTEXT.resolve(Repository.class);
  }

  public void setOutbound(Outbound outbound) {
    _outbound = outbound;
  }

  public void applyTo(InboundMessage message) {
    message.authorizeUsing(_authorizationRules);
    _repository.save(message);
    _outbound.send(message);
  }
}
