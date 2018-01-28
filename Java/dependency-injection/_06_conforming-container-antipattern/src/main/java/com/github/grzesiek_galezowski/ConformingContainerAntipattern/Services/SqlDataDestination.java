package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.DataDestination;

public class SqlDataDestination implements DataDestination {
  public void add(String content) {
    throw new RuntimeException("not implemented");
  }
}
