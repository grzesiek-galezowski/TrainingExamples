package com.example.gui;

import com.example.ports.views.Application;

public class Window {
  private final Application domainLogic;

  public Window(final Application domainLogic) {
    this.domainLogic = domainLogic;
  }

  public void onSubmitClicked()
  {
    domainLogic.handleAddEmployeeRequest();
  }
}
