package ServiceLocatorAntipattern.Core;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Interfaces.Message;
import ServiceLocatorAntipattern.Outbound.Outbound;
import ServiceLocatorAntipattern.Services.Authorization;
import ServiceLocatorAntipattern.Services.Repository;

public class AcmeProcessingWorkflow implements ProcessingWorkflow {
  private final Repository repository;
  private final Authorization authorizationRules;
  private Outbound outbound;

  public AcmeProcessingWorkflow() {
    authorizationRules = ApplicationRoot.context.getComponent(Authorization.class);
    repository = ApplicationRoot.context.getComponent(Repository.class);
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
