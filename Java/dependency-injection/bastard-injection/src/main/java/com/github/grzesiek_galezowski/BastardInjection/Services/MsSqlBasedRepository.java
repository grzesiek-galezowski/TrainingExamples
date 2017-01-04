package com.github.grzesiek_galezowski.BastardInjection.Services;

import com.github.grzesiek_galezowski.BastardInjection.Interfaces.Message;
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

  public void save(Message message) {
    message.writeTo(_dataDestination);
  }
}

