package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Core;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound.Outbound;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services.Authorization;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services.Repository;

public class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final Repository repository;
  private final Authorization authorizationRules;
  private Outbound outbound;

  public AcmeProcessingWorkflow() {
    authorizationRules = ApplicationRoot.CONTEXT.resolve(Authorization.class);
    repository = ApplicationRoot.CONTEXT.resolve(Repository.class);
  }

  public void setOutbound(Outbound outbound) {
    this.outbound = outbound;
  }

  public void applyTo(InboundMessage message) {
    message.authorizeUsing(authorizationRules);
    repository.save(message);
    outbound.send(message);
  }
}
