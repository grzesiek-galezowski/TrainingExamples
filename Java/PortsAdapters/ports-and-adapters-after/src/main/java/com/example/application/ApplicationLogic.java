package com.example.application;

import com.example.ports.dataaccess.Repository;
import com.example.ports.views.Application;

public class ApplicationLogic implements Application {
  private final Repository repository;

  public ApplicationLogic(final Repository repository) {
    this.repository = repository;
  }

  @Override
  public void handleAddEmployeeRequest()
  {
    repository.saveEmployee();
  }
}
