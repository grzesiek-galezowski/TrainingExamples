package org.github.grzesiekgalezowski.examples.domain;

import org.github.grzesiekgalezowski.examples.config.Cache;
import org.springframework.stereotype.Component;

@Component
public class Destination implements Output {
  public Destination(final Cache cache, Source source) {
  }
}
