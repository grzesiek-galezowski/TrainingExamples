package com.github.grzesiek_galezowski.BastardInjection.Services;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.AcmeMessage;
import com.github.grzesiek_galezowski.BastardInjection.Interfaces.DataDestination;

public class MsSqlBasedRepository implements IRepository {
  private final DataDestination _dataDestination;

  public MsSqlBasedRepository() {
    this(new SqlDataDestination());
  }

  //for tests
  public MsSqlBasedRepository(DataDestination dataDestination) {
    _dataDestination = dataDestination;
  }

  public void save(AcmeMessage message) {
    message.writeTo(_dataDestination);
  }
}

