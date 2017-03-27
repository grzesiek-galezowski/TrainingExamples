package com.github.grzesiek_galezowski.BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection.Inbound.BinaryUdpInbound;
import com.github.grzesiek_galezowski.BastardInjection.Inbound.Inbound;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.Outbound;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.XmlOutbound;

import java.io.Closeable;
import java.io.IOException;

public class TeleComSystem implements Closeable {
  private final ProcessingWorkflow _processingWorkflow;
  private final Inbound _inbound;
  private final Outbound _outbound;

  public TeleComSystem() {
    this(new AcmeProcessingWorkflow(), new BinaryUdpInbound(), new XmlOutbound());
  }

  //for tests
  public TeleComSystem(
      ProcessingWorkflow processingWorkflow,
      Inbound inbound,
      Outbound outbound) {
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
