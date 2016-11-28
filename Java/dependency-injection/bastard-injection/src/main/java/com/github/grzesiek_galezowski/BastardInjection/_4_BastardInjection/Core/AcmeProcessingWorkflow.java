package com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Core;

interface IProcessingWorkflow
  {
    void SetOutbound(IOutbound outbound);
    void ApplyTo(AcmeMessage message);
  }

  class AcmeProcessingWorkflow implements IProcessingWorkflow
  {
    private final IRepository _repository;
    private final IAuthorization _authorizationRules;
    private IOutbound _outbound;

    public AcmeProcessingWorkflow()
    {
      this(new ActiveDirectoryBasedAuthorization(), new MsSqlBasedRepository());
    }

    //for tests
    private AcmeProcessingWorkflow(
            IAuthorization authorization,
            IRepository repository)
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