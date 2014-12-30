using System;
using BastardInjection._4_BastardInjection.Inbound;
using BastardInjection._4_BastardInjection.Outbound;

namespace BastardInjection._4_BastardInjection.Core
{
  class TeleComSystem : IDisposable
  {
    private readonly IProcessingWorkflow _processingWorkflow;
    private readonly IInbound _inbound;
    private readonly IOutbound _outbound;

    public TeleComSystem()
      : this(new AcmeProcessingWorkflow(), new BinaryUdpInbound(), new Outbound.Outbound())
    {
      
    }

    //for tests
    public TeleComSystem(
      IProcessingWorkflow processingWorkflow,
      IInbound inbound,
      IOutbound outbound)
    {
      _processingWorkflow = processingWorkflow;
      _inbound = inbound;
      _outbound = outbound;
    }

    public void Start()
    {
      _inbound.SetDomainLogic(_processingWorkflow);
      _processingWorkflow.SetOutbound(_outbound);
      _inbound.StartListening();
    }

    public void Dispose()
    {
      _inbound.Dispose();
    }
  }
}