package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.DataDestination;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

public class SqlDataDestination implements DataDestination {
  public void add(String content) {
    throw new NotImplementedException();
  }
}
