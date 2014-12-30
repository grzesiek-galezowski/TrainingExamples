using BastardInjection._4_BastardInjection.Interfaces;
using BastardInjection._4_BastardInjection.Outbound;
using BastardInjection._4_BastardInjection.Services;

namespace BastardInjection._4_BastardInjection.Core
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

    public AcmeProcessingWorkflow() : this(new ActiveDirectoryBasedAuthorization(), new MsSqlBasedRepository())
    {
    }

    //for tests
    private AcmeProcessingWorkflow(IAuthorization authorization, IRepository repository)
    {
      _authorizationRules = authorization;
      _repository = repository;
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