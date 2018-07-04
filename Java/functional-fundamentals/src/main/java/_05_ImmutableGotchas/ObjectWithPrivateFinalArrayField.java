package _05_ImmutableGotchas;

public final class ObjectWithPrivateFinalArrayField {
    private final Integer[] elements;

    public ObjectWithPrivateFinalArrayField(final Integer[] elements) {
        this.elements = elements;
    }
}
