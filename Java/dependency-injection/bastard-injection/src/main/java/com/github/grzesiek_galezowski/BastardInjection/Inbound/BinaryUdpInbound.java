package com.github.grzesiek_galezowski.BastardInjection.Inbound;

import com.github.grzesiek_galezowski.BastardInjection.Core.IProcessingWorkflow;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;

import java.io.IOException;

public class BinaryUdpInbound implements IInbound {
    private IProcessingWorkflow _processingWorkflow;
    private final IInputSocket _socket;
    private final IPacketParsing _parsing;

    public BinaryUdpInbound() {
      this(new UdpSocket(), new BinaryParsing());
    }

    //for tests
    public BinaryUdpInbound(IInputSocket socket, IPacketParsing parsing) {
      _socket = socket;
      _parsing = parsing;
    }

    public void setDomainLogic(IProcessingWorkflow processingWorkflow) {
      _processingWorkflow = processingWorkflow;
    }

    public void startListening() {
      byte[] frameData = new byte[100];
      while (_socket.receive(frameData)) {
        AcmeMessage message = _parsing.resultFor(frameData);
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
