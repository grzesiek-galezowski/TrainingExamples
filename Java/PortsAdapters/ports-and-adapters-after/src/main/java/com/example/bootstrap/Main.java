package com.example.bootstrap;

import com.example.application.ApplicationLogic;
import com.example.database.DatabaseObject;
import com.example.gui.Window;

public class Main {

  private static Window window;
  private static DatabaseObject repository;
  private static ApplicationLogic domainLogic;

  public static void Main() {
    repository = new DatabaseObject();
    domainLogic = new ApplicationLogic(repository);
    window = new Window(domainLogic);
    window.OnSubmitClicked();
  }
}
