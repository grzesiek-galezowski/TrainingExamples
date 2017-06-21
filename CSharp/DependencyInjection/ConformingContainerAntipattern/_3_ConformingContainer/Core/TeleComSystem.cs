using ConformingContainerAntipattern._3_ConformingContainer.Inbound;
using ConformingContainerAntipattern._3_ConformingContainer.Outbound;

namespace ConformingContainerAntipattern._3_ConformingContainer.Core
{
  class TeleComSystem
  {
    private readonly IProcessingWorkflow _processingWorkflow;
    private readonly IInbound _inbound;
    private readonly IOutbound _outbound;

    public TeleComSystem()
    {
      _inbound = ApplicationRoot.Context.Resolve<IInbound>();
      _outbound = ApplicationRoot.Context.Resolve<IOutbound>();
      _processingWorkflow = ApplicationRoot.Context.Resolve<IProcessingWorkflow>();
    }

    public void Start()
    {
      _inbound.SetDomainLogic(_processingWorkflow);
      _processingWorkflow.SetOutbound(_outbound);
      _inbound.StartListening();
    }
  }
}