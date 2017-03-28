package com.github.grzesiek_galezowski.BastardInjection.Inbound;

import com.github.grzesiek_galezowski.BastardInjection.Core.ProcessingWorkflow;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;

import java.io.IOException;

public class BinaryUdpInbound implements Inbound {
    private ProcessingWorkflow processingWorkflow;
    private final InputSocket socket;
    private final PacketParsing parsing;

    public BinaryUdpInbound() {
      this(new UdpSocket(), new BinaryParsing());
    }

    //for tests
    public BinaryUdpInbound(InputSocket socket, PacketParsing parsing) {
      this.socket = socket;
      this.parsing = parsing;
    }

    public void setDomainLogic(ProcessingWorkflow processingWorkflow) {
      this.processingWorkflow = processingWorkflow;
    }

    public void startListening() {
      byte[] frameData = new byte[100];
      while (socket.receive(frameData)) {
        Message message = parsing.resultFor(frameData);
        if (message != null) {
          if (processingWorkflow != null) {
            processingWorkflow.applyTo(message);
          }
        }
      }
    }

    public void close() throws IOException {
      socket.close(); //error!
    }
  }
