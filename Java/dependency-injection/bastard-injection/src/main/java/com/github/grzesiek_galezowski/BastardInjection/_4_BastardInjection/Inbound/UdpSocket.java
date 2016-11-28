package com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Inbound;

import com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Core.IDisposable;

interface IInputSocket extends IDisposable //first error - do not implement disposabled in interfaces
  {
    boolean Receive(byte[] frameData);
  }

  class UdpSocket implements IInputSocket
  {
    public boolean Receive(byte[] frameData)
    {
      //todo generate random the java way new Random().NextBytes(frameData);
      return true;
    }

    public void Dispose()
    {
      System.out.println("Disposed");
    }
  }
