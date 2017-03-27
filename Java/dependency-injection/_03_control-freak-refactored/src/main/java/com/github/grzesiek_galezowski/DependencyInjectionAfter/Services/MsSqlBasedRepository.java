package com.github.grzesiek_galezowski.DependencyInjectionAfter.Services;

import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.DependencyInjectionAfter.Interfaces.DataDestination;

public class MsSqlBasedRepository implements IRepository {
  private final DataDestination _sqlDataDestination;

  public MsSqlBasedRepository(DataDestination sqlDataDestination) {
    _sqlDataDestination = sqlDataDestination;
  }

  public void Save(AcmeMessage message) {
    message.WriteTo(_sqlDataDestination);
  }
}
