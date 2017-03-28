package com.github.grzesiek_galezowski.DependencyInjectionAfter.Core;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.Outbound;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.Authorization;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.Repository;

public class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final Repository repository;
  private final Authorization authorizationRules;
  private Outbound outbound;

  public AcmeProcessingWorkflow(
      Authorization activeDirectoryBasedAuthorization,
      Repository msSqlBasedRepository) {
    authorizationRules = activeDirectoryBasedAuthorization;
    repository = msSqlBasedRepository;
  }

  public void SetOutbound(Outbound outbound) {
    this.outbound = outbound;
  }

  public void ApplyTo(AcmeMessage message) {
    message.AuthorizeUsing(authorizationRules);
    repository.Save(message);
    outbound.Send(message);
  }
}
