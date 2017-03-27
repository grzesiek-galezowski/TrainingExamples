package com.github.grzesiek_galezowski.ConformingContainerAntipattern.Outbound;

public class XmlMarshalling implements Marshalling {
  public String of(String arg) {
    return "<" + arg + ">";
  }
}
