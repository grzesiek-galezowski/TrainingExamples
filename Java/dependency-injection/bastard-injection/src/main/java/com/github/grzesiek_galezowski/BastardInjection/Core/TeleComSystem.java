package com.github.grzesiek_galezowski.BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection.Inbound.BinaryUdpInbound;
import com.github.grzesiek_galezowski.BastardInjection.Inbound.IInbound;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.IOutbound;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.Outbound;

import java.io.Closeable;
import java.io.IOException;

public class TeleComSystem implements Closeable {
  private final IProcessingWorkflow _processingWorkflow;
  private final IInbound _inbound;
  private final IOutbound _outbound;

  public TeleComSystem() {
    this(new AcmeProcessingWorkflow(), new BinaryUdpInbound(), new Outbound());
  }

  //for tests
  public TeleComSystem(
      IProcessingWorkflow processingWorkflow,
      IInbound inbound,
      IOutbound outbound) {
    _processingWorkflow = processingWorkflow;
    _inbound = inbound;
    _outbound = outbound;
  }

  public void start() {
    _inbound.setDomainLogic(_processingWorkflow);
    _processingWorkflow.setOutbound(_outbound);
    _inbound.startListening();
  }

  public void close() throws IOException {
    _inbound.close();
  }
}
