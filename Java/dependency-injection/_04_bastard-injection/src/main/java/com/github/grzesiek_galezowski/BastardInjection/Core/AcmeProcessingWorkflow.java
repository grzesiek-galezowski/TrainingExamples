package com.github.grzesiek_galezowski.BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.Outbound;
import com.github.grzesiek_galezowski.BastardInjection.Services.ActiveDirectoryBasedAuthorization;
import com.github.grzesiek_galezowski.BastardInjection.Services.IAuthorization;
import com.github.grzesiek_galezowski.BastardInjection.Services.IRepository;
import com.github.grzesiek_galezowski.BastardInjection.Services.MsSqlBasedRepository;

class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final IRepository _repository;
  private final IAuthorization _authorizationRules;
  private Outbound _outbound;

  public AcmeProcessingWorkflow() {
    this(new ActiveDirectoryBasedAuthorization(), new MsSqlBasedRepository());
  }

  //for tests
  public AcmeProcessingWorkflow(
      IAuthorization authorization,
      IRepository repository) {
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
