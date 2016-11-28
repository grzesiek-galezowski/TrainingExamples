package com.github.grzesiek_galezowski.BastardInjection.Inbound;

import com.github.grzesiek_galezowski.BastardInjection.Core.IProcessingWorkflow;

import java.io.Closeable;

/**
 * Created by grzes on 28.11.2016.
 */
public interface IInbound extends Closeable //TODO error! forced by the UdpSocket
  {
    void setDomainLogic(IProcessingWorkflow processingWorkflow);
    void startListening();
  }
