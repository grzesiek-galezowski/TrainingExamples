package com.github.grzesiek_galezowski.BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.Outbound;
import com.github.grzesiek_galezowski.BastardInjection.Services.ActiveDirectoryBasedAuthorization;
import com.github.grzesiek_galezowski.BastardInjection.Services.Authorization;
import com.github.grzesiek_galezowski.BastardInjection.Services.Repository;
import com.github.grzesiek_galezowski.BastardInjection.Services.MsSqlBasedRepository;

class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final Repository _repository;
  private final Authorization _authorizationRules;
  private Outbound _outbound;

  public AcmeProcessingWorkflow() {
    this(new ActiveDirectoryBasedAuthorization(), new MsSqlBasedRepository());
  }

  //for tests
  public AcmeProcessingWorkflow(
      Authorization authorization,
      Repository repository) {
    _authorizationRules = authorization;
    _repository = repository;
  }

  public void setOutbound(Outbound outbound) {
    _outbound = outbound;
  }

  public void applyTo(Message message) {
    message.authorizeUsing(_authorizationRules);
    _repository.save(message);
    _outbound.send(message);
  }
}
