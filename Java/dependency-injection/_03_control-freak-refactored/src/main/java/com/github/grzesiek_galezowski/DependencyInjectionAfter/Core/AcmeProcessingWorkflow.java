package com.github.grzesiek_galezowski.DependencyInjectionAfter.Core;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound.IOutbound;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.IAuthorization;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Services.IRepository;

public class AcmeProcessingWorkflow implements IAcmeProcessingWorkflow {
  private final IRepository _repository;
  private final IAuthorization _authorizationRules;
  private IOutbound _outbound;

  public AcmeProcessingWorkflow(
      IAuthorization activeDirectoryBasedAuthorization,
      IRepository msSqlBasedRepository) {
    _authorizationRules = activeDirectoryBasedAuthorization;
    _repository = msSqlBasedRepository;
  }

  public void SetOutbound(IOutbound outbound) {
    _outbound = outbound;
  }

  public void ApplyTo(AcmeMessage message) {
    message.AuthorizeUsing(_authorizationRules);
    _repository.Save(message);
    _outbound.Send(message);
  }
}
