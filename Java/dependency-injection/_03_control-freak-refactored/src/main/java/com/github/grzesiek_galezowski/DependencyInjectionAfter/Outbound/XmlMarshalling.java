package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public class XmlMarshalling implements Marshalling {
  public String Of(String arg) {
    return "<" + arg + ">";
  }
}
