package org.github.grzesiekgalezowski.examples.domain;

import org.github.grzesiekgalezowski.examples.config.Cache;
import org.springframework.stereotype.Component;

@Component
public class Destination implements Output {
  public Destination(final Cache cache, Source source) {
    System.out.println(this.getClass() + ": ");
    System.out.println("-> " + cache.getClass());
    System.out.println("-> " + source.getClass());
  }
}
