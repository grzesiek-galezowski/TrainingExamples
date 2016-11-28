using ConformingContainerAntipattern._3_ConformingContainer.Interfaces;
using ConformingContainerAntipattern._3_ConformingContainer.Outbound;
using ConformingContainerAntipattern._3_ConformingContainer.Services;

namespace ConformingContainerAntipattern._3_ConformingContainer.Core
{
  internal interface IProcessingWorkflow
  {
    void SetOutbound(IOutbound outbound);
    void ApplyTo(AcmeMessage message);
  }

  class AcmeProcessingWorkflow : IProcessingWorkflow
  {
    private readonly IRepository _repository;
    private readonly IAuthorization _authorizationRules;
    private IOutbound _outbound;

    public AcmeProcessingWorkflow()
    {
      _authorizationRules = ApplicationRoot.Context.Resolve<IAuthorization>();
      _repository = ApplicationRoot.Context.Resolve<IRepository>();
    }

    public void SetOutbound(IOutbound outbound)
    {
      _outbound = outbound;
    }

    public void ApplyTo(AcmeMessage message)
    {
      message.AuthorizeUsing(_authorizationRules);
      _repository.Save(message);
      _outbound.Send(message);
    }
  }
}