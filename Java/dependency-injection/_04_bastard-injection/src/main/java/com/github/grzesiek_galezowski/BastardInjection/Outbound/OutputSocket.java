package com.github.grzesiek_galezowski.BastardInjection.Outbound;

/**
 * Created by grzes on 28.11.2016.
 */
interface OutputSocket
  {
    void open();
    void close();
    void send(String lol);
  }
