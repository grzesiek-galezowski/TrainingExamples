package ServiceLocatorAntipattern.Services;

import ServiceLocatorAntipattern.Interfaces.DataDestination;
import sun.reflect.generics.reflectiveObjects.NotImplementedException;

public class SqlDataDestination implements DataDestination {
  public void add(String content) {
    throw new NotImplementedException();
  }
}