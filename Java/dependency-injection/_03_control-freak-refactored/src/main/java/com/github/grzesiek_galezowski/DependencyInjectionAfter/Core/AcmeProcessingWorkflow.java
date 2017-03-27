package com.github.grzesiek_galezowski.DependencyInjectionAfter.Core;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.Outbound;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.Authorization;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.Repository;

public class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final Repository _repository;
  private final Authorization _authorizationRules;
  private Outbound _outbound;

  public AcmeProcessingWorkflow(
      Authorization activeDirectoryBasedAuthorization,
      Repository msSqlBasedRepository) {
    _authorizationRules = activeDirectoryBasedAuthorization;
    _repository = msSqlBasedRepository;
  }

  public void SetOutbound(Outbound outbound) {
    _outbound = outbound;
  }

  public void ApplyTo(AcmeMessage message) {
    message.AuthorizeUsing(_authorizationRules);
    _repository.Save(message);
    _outbound.Send(message);
  }
}
