package _05_ImmutableGotchas;

import java.util.ArrayList;

public final class ObjectWithPrivateFinalListPassedViaConstructor {
    private final ArrayList<Integer> list;

    public ObjectWithPrivateFinalListPassedViaConstructor(
        ArrayList<Integer> list) {

        this.list = list;
    }
}
