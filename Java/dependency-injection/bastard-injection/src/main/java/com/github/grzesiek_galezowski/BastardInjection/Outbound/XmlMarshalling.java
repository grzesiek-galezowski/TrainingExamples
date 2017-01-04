package com.github.grzesiek_galezowski.BastardInjection.Outbound;

class XmlMarshalling implements Marshalling {
  public String Of(String arg) {
    return "<" + arg + ">";
  }
}
