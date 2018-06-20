package _04_composition.wrappers;

import java.util.function.Function;

public class Compose {

    public static <T, U, V> Function<T, V> compose(
        Function<T, U> f,
        Function<U, V> g) {

        return instance -> g.apply(f.apply(instance));
    }

}
