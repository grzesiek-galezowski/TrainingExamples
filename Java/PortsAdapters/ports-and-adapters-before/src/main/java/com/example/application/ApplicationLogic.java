package com.example.application;

import com.example.database.DatabaseObject;

public class ApplicationLogic {
  private final DatabaseObject database = new DatabaseObject();

  public void handleAddEmployeeRequest()
  {
    database.saveEmployee();
  }
}
