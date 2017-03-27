package com.github.grzesiek_galezowski.BastardInjection.Inbound;

import java.io.Closeable;

/**
 * Created by grzes on 28.11.2016.
 */
interface InputSocket extends Closeable //first error - do not implement disposabled in interfaces
  {
    boolean receive(byte[] frameData);
  }
