package _05_ImmutableGotchas;

public final class ObjectWithPrivateFinalGenericField<T> {
    private final T field;

    public ObjectWithPrivateFinalGenericField(final T field) {
        this.field = field;
    }
}
