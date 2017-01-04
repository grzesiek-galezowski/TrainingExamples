package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Services;

import com.github.grzesiek_galezowski.ConformingContainerAntipattern.ApplicationRoot;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.InboundMessage;
import com.github.grzesiek_galezowski.ConformingContainerAntipattern.Interfaces.DataDestination;

public class MsSqlBasedRepository implements Repository {
  private final DataDestination _sqlDataDestination = ApplicationRoot.CONTEXT.resolve(SqlDataDestination.class);

  public void save(InboundMessage message) {
    message.writeTo(_sqlDataDestination);
  }
}
