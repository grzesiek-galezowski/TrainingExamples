package com.github.grzesiek_galezowski.BastardInjection.Core;

import com.github.grzesiek_galezowski.BastardInjection.Inbound.BinaryUdpInbound;
import com.github.grzesiek_galezowski.BastardInjection.Inbound.Inbound;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.Outbound;
import com.github.grzesiek_galezowski.BastardInjection.Outbound.XmlOutbound;

import java.io.Closeable;
import java.io.IOException;

public class TeleComSystem implements Closeable {
  private final ProcessingWorkflow processingWorkflow;
  private final Inbound inbound;
  private final Outbound outbound;

  public TeleComSystem() {
    this(new AcmeProcessingWorkflow(), new BinaryUdpInbound(), new XmlOutbound());
  }

  //for tests
  public TeleComSystem(
      ProcessingWorkflow processingWorkflow,
      Inbound inbound,
      Outbound outbound) {
    this.processingWorkflow = processingWorkflow;
    this.inbound = inbound;
    this.outbound = outbound;
  }

  public void start() {
    inbound.setDomainLogic(processingWorkflow);
    processingWorkflow.setOutbound(outbound);
    inbound.startListening();
  }

  public void close() throws IOException {
    inbound.close();
  }
}
