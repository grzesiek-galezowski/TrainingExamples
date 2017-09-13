package org.github.grzesiekgalezowski.examples.domain;

import org.springframework.stereotype.Component;

@Component
public class Destination implements Output {
  public Destination(Source source) {
  }
}
