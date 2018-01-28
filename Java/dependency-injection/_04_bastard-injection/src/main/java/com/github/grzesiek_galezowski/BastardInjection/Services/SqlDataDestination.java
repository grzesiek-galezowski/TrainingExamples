package com.github.grzesiek_galezowski.BastardInjection.Services;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.DataDestination;

class SqlDataDestination implements DataDestination {
  public void add(String content) {
    throw new RuntimeException("not implemented");
  }
}

