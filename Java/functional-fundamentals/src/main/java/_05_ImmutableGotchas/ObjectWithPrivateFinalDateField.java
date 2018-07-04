package _05_ImmutableGotchas;

import java.util.Date;

public final class ObjectWithPrivateFinalDateField {
    private final Date date = new Date();

    public Date getDate() {
        return date;
    }
}
