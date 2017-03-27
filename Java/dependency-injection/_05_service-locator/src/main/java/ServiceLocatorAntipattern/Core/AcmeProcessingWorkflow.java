package ServiceLocatorAntipattern.Core;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Interfaces.Message;
import ServiceLocatorAntipattern.Outbound.Outbound;
import ServiceLocatorAntipattern.Services.Authorization;
import ServiceLocatorAntipattern.Services.Repository;

public class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final Repository _repository;
  private final Authorization _authorizationRules;
  private Outbound _outbound;

  public AcmeProcessingWorkflow() {
    _authorizationRules = ApplicationRoot.context.getComponent(Authorization.class);
    _repository = ApplicationRoot.context.getComponent(Repository.class);
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
