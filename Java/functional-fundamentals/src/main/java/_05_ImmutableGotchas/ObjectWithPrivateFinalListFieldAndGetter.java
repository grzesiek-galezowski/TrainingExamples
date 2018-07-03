package _05_ImmutableGotchas;

import java.util.ArrayList;

public final class ObjectWithPrivateFinalListFieldAndGetter {
    private final ArrayList<Integer> list = new ArrayList<>();

    public ArrayList<Integer> getList() {
        return list;
    }
}
