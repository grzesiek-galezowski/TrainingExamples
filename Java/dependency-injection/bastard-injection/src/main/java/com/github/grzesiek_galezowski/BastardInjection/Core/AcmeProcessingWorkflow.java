package com.github.grzesiek_galezowski.BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.IOutbound;
import com.github.grzesiek_galezowski.BastardInjection.Services.ActiveDirectoryBasedAuthorization;
import com.github.grzesiek_galezowski.BastardInjection.Services.IAuthorization;
import com.github.grzesiek_galezowski.BastardInjection.Services.IRepository;
import com.github.grzesiek_galezowski.BastardInjection.Services.MsSqlBasedRepository;

class AcmeProcessingWorkflow implements IProcessingWorkflow {
  private final IRepository _repository;
  private final IAuthorization _authorizationRules;
  private IOutbound _outbound;

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

  public void setOutbound(IOutbound outbound) {
    _outbound = outbound;
  }

  public void applyTo(AcmeMessage message) {
    message.authorizeUsing(_authorizationRules);
    _repository.save(message);
    _outbound.send(message);
  }
}
