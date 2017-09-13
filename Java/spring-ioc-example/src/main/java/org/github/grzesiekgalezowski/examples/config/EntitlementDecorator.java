package org.github.grzesiekgalezowski.examples.config;

import org.github.grzesiekgalezowski.examples.domain.Entitlement;

public class EntitlementDecorator extends Entitlement {
  public EntitlementDecorator(final Entitlement e, final String str) {
    super(e);
  }
}
