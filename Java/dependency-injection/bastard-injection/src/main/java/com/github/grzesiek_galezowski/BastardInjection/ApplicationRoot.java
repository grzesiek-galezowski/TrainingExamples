package com.github.grzesiek_galezowski.BastardInjection;

import com.github.grzesiek_galezowski.BastardInjection.Core.TeleComSystem;

import java.io.IOException;

public class ApplicationRoot {
  public static void main(String[] args) throws IOException {
    TeleComSystem sys = new TeleComSystem(); //bastard injected - look at output socket which is disposable in this example!
    sys.start();

    sys.close(); //!!!!!!!!!!!!!!!!!!!!!!!!!!!! error!!!!
  }
}





