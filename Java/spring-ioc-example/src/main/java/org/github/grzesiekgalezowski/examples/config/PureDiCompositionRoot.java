package org.github.grzesiekgalezowski.examples.config;

import org.github.grzesiekgalezowski.examples.domain.*;

public class PureDiCompositionRoot {
  public Entitlement getMyEntitlement() {
    return new EntitlementDecorator(
        new BusinessEntitlement(
            new InMemoryCache(),
            new Destination(new PersistentCache(),
                new Source(new PersistentCache() /* new instance! */)),
            "lolek112"),
        "lolek");
  }

}
