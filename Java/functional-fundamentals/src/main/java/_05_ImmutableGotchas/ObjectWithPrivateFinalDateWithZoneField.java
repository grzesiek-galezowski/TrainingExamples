package _05_ImmutableGotchas;

import java.time.ZonedDateTime;

public final class ObjectWithPrivateFinalDateWithZoneField {
    private final ZonedDateTime date = ZonedDateTime.now();
}
