package composition2;

import java.util.function.Function;

import static java.lang.System.out;

public class Compose {

    public static <T, U, V> Function<T, V> compose(
        Function<T, U> f,
        Function<U, V> g) {

        return instance -> g.apply(f.apply(instance));
    }

    public static <T, U> Function<T, U> fun(Function<T, U> f) {
        return f;
    }

    public static void main(String[] args) {
        out.println(((((0) + 1) + 1) + 1) + 1);

        out.println(compose(Demo::plus1, Demo::plus1).apply(0));

        out.println(
            compose(
                Demo::plus1,
                compose(Demo::plus1, Demo::plus1)
            ).apply(0));


        out.println(
            compose(
                Demo::plus1,
                compose(
                    Demo::plus1,
                    compose(
                        Demo::plus1,
                        Demo::plus1)
                )
            ).apply(0));

        out.println(
            compose(
                Demo::plus1,
                compose(
                    Demo::plus1,
                    compose(
                        Demo::plus1,
                        compose(
                            Demo::plus1,
                            Demo::plus1)
                    )
                )
            ).apply(0));


        out.println(
            fun(
                Demo::plus1)
                .andThen(Demo::plus1)
                .andThen(Demo::plus1)
                .andThen(Demo::plus1)
                .andThen(Demo::plus1)
                .apply(0));
    }

}
