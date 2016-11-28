package com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Core.IDisposable;

class TeleComSystem implements IDisposable
  {
    private final IProcessingWorkflow _processingWorkflow;
    private final IInbound _inbound;
    private final IOutbound _outbound;

    public TeleComSystem()
    {
      this(new AcmeProcessingWorkflow(), new BinaryUdpInbound(), new Outbound());
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