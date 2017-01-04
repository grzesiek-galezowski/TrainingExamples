package ServiceLocatorAntipattern.Core;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Interfaces.Message;
import ServiceLocatorAntipattern.Outbound.Outbound;
import ServiceLocatorAntipattern.Services.IAuthorization;
import ServiceLocatorAntipattern.Services.IRepository;

public class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final IRepository _repository;
  private final IAuthorization _authorizationRules;
  private Outbound _outbound;

  public AcmeProcessingWorkflow() {
    _authorizationRules = ApplicationRoot.context.getComponent(IAuthorization.class);
    _repository = ApplicationRoot.context.getComponent(IRepository.class);
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
