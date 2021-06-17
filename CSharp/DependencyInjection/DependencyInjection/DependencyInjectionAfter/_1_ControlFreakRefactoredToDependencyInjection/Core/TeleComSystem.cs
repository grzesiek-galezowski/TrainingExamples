using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Inbound;
using DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Outbound;

namespace DependencyInjectionAfter._1_ControlFreakRefactoredToDependencyInjection.Core
{
  class TeleComSystem
  {
    private readonly IAcmeProcessingWorkflow _processingWorkflow;
    private readonly IInbound _inbound;
    private readonly IOutbound _outbound;

    public TeleComSystem(
      IInbound binaryUdpInbound, 
      IOutbound xmlTcpOutbound, 
      IAcmeProcessingWorkflow acmeProcessingWorkflow)
    {
      _inbound = binaryUdpInbound;
      _outbound = xmlTcpOutbound;
      _processingWorkflow = acmeProcessingWorkflow;
    }

    public void Start()
    {
      _inbound.SetDomainLogic(_processingWorkflow);
      _processingWorkflow.SetOutbound(_outbound);
      _inbound.StartListening();
    }
  }
}