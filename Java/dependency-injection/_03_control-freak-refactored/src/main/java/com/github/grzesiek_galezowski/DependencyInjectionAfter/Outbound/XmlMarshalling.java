package com.github.grzesiek_galezowski.DependencyInjectionAfter.Outbound;

public class XmlMarshalling implements IMarshalling {
  public String Of(String arg) {
    return "<" + arg + ">";
  }
}
