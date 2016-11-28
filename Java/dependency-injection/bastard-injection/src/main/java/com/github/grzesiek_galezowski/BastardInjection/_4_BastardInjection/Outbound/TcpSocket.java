package com.github.grzesiek_galezowski.BastardInjection._4_BastardInjection.Outbound;

interface IOutputSocket
  {
    void Open();
    void Close();
    void Send(String lol);
  }

  class TcpSocket implements IOutputSocket {
    public void Open() {
      System.out.println("open");
    }

    public void Close() {
      System.out.println("closing");
    }

    public void Send(String lol) {
      System.out.println(lol);
    }
  }
