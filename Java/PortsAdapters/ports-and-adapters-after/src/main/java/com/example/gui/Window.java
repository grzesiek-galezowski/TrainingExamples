package com.example.gui;

import com.example.ports.views.Backend;

public class Window {
  private final Backend domainLogic;

  public Window(final Backend domainLogic) {
    this.domainLogic = domainLogic;
  }

  public void onSubmitClicked()
  {
    domainLogic.handleAddEmployeeRequest();
  }
}
