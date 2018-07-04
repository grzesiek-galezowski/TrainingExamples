package _05_ImmutableGotchas;

public final class ObjectWithPrivateFinalAbstractField {

    private final AbstractWhatever whatever;

    public ObjectWithPrivateFinalAbstractField(
        final AbstractWhatever whatever) {

        this.whatever = whatever;
    }
}

abstract class AbstractWhatever {

}
