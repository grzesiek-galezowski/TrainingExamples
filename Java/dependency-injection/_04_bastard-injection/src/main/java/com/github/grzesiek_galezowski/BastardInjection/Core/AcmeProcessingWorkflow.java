package com.github.grzesiek_galezowski.BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.Outbound;
import com.github.grzesiek_galezowski.BastardInjection.Services.ActiveDirectoryBasedAuthorization;
import com.github.grzesiek_galezowski.BastardInjection.Services.Authorization;
import com.github.grzesiek_galezowski.BastardInjection.Services.Repository;
import com.github.grzesiek_galezowski.BastardInjection.Services.MsSqlBasedRepository;

class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final Repository repository;
  private final Authorization authorizationRules;
  private Outbound outbound;

  public AcmeProcessingWorkflow() {
    this(new ActiveDirectoryBasedAuthorization(), new MsSqlBasedRepository());
  }

  //for tests
  public AcmeProcessingWorkflow(
      Authorization authorization,
      Repository repository) {
    authorizationRules = authorization;
    this.repository = repository;
  }

  public void setOutbound(Outbound outbound) {
    this.outbound = outbound;
  }

  public void applyTo(Message message) {
    message.authorizeUsing(authorizationRules);
    repository.save(message);
    outbound.send(message);
  }
}
