using DependencyInjectionBefore._1_ControlFreak.Interfaces;
using DependencyInjectionBefore._1_ControlFreak.Outbound;
using DependencyInjectionBefore._1_ControlFreak.Services;

namespace DependencyInjectionBefore._1_ControlFreak.Core
{
  class AcmeProcessingWorkflow
  {
    private readonly MsSqlBasedRepository _repository;
    private readonly ActiveDirectoryBasedAuthorization _authorizationRules;
    private XmlTcpOutbound _outbound;

    public AcmeProcessingWorkflow()
    {
      _authorizationRules = new ActiveDirectoryBasedAuthorization();
      _repository = new MsSqlBasedRepository();
    }

    public void SetOutbound(XmlTcpOutbound outbound)
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