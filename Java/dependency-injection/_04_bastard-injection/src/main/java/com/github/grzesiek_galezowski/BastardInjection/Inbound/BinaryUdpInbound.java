package com.github.grzesiek_galezowski.BastardInjection.Inbound;

import com.github.grzesiek_galezowski.BastardInjection.Core.ProcessingWorkflow;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;

import java.io.IOException;

public class BinaryUdpInbound implements Inbound {
    private ProcessingWorkflow _processingWorkflow;
    private final InputSocket _socket;
    private final PacketParsing _parsing;

    public BinaryUdpInbound() {
      this(new UdpSocket(), new BinaryParsing());
    }

    //for tests
    public BinaryUdpInbound(InputSocket socket, PacketParsing parsing) {
      _socket = socket;
      _parsing = parsing;
    }

    public void setDomainLogic(ProcessingWorkflow processingWorkflow) {
      _processingWorkflow = processingWorkflow;
    }

    public void startListening() {
      byte[] frameData = new byte[100];
      while (_socket.receive(frameData)) {
        Message message = _parsing.resultFor(frameData);
        if (message != null) {
          if (_processingWorkflow != null) {
            _processingWorkflow.applyTo(message);
          }
        }
      }
    }

    public void close() throws IOException {
      _socket.close(); //error!
    }
  }
