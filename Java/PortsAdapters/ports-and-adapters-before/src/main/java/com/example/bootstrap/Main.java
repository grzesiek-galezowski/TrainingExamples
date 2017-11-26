package com.example.bootstrap;

import com.example.gui.Window;

public class Main {

  private static Window window;

  public static void Main() {
    window = new Window();
    window.OnSubmitClicked();
  }
}
