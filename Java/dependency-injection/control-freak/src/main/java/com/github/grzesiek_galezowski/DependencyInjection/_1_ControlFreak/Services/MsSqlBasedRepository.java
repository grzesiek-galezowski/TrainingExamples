package com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Services;

import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjection._1_ControlFreak.Interfaces.DataDestination;

public class MsSqlBasedRepository {
  private final DataDestination _sqlDataDestination = new SqlDataDestination();

  public void save(AcmeMessage message) {
    message.writeTo(_sqlDataDestination);
  }
}
