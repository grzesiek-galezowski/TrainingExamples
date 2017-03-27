package ServiceLocatorAntipattern.Services;

import ServiceLocatorAntipattern.ApplicationRoot;
import ServiceLocatorAntipattern.Interfaces.Message;
import ServiceLocatorAntipattern.Interfaces.DataDestination;

public class MsSqlBasedRepository implements IRepository {
  private final DataDestination _sqlDataDestination = ApplicationRoot.context.getComponent(SqlDataDestination.class);

  public void save(Message message) {
    message.writeTo(_sqlDataDestination);
  }
}
