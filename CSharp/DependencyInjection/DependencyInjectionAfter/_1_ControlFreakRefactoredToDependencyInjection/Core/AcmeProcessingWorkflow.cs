using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Interfaces;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Outbound;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Services;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Core
{
  internal interface IAcmeProcessingWorkflow
  {
    void SetOutbound(IOutbound outbound);
    void ApplyTo(AcmeMessage message);
  }

  class AcmeProcessingWorkflow : IAcmeProcessingWorkflow
  {
    private readonly IRepository _repository;
    private readonly IAuthorization _authorizationRules;
    private IOutbound _outbound;

    public AcmeProcessingWorkflow(
      IAuthorization activeDirectoryBasedAuthorization, 
      IRepository msSqlBasedRepository)
    {
      _authorizationRules = activeDirectoryBasedAuthorization;
      _repository = msSqlBasedRepository;
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