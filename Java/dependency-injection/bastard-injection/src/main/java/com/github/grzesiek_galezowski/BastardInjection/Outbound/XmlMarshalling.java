package com.github.grzesiek_galezowski.BastardInjection.Outbound;

class XmlMarshalling implements IMarshalling {
  public String Of(String arg) {
    return "<" + arg + ">";
  }
}
