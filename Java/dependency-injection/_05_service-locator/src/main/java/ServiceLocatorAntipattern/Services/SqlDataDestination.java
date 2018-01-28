package ServiceLocatorAntipattern.Services;

import ServiceLocatorAntipattern.Interfaces.DataDestination;

public class SqlDataDestination implements DataDestination {
  public void add(String content) {
    throw new RuntimeException("not implemented");
  }
}