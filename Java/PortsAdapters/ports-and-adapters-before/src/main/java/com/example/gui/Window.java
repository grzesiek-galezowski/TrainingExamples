package com.example.gui;

import com.example.application.ApplicationLogic;

public class Window {
  private final ApplicationLogic domainLogic = new ApplicationLogic();

  public void OnSubmitClicked()
  {
    domainLogic.handleAddEmployeeRequest();
  }
}
