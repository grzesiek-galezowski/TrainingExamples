package org.github.grzesiekgalezowski.examples.domain;

import org.github.grzesiekgalezowski.examples.config.Cache;
import org.springframework.context.annotation.Lazy;
import org.springframework.stereotype.Component;

@Component
public class Source {
  public Source(final Cache cache) {
    System.out.println(this.getClass() + ": ");
    System.out.println("-> " + cache.getClass());
  }
}
