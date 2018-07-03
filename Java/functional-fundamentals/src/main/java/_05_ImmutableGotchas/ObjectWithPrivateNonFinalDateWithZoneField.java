package _05_ImmutableGotchas;

import java.time.ZonedDateTime;

public final class ObjectWithPrivateNonFinalDateWithZoneField {
    private ZonedDateTime date = ZonedDateTime.now();
}
