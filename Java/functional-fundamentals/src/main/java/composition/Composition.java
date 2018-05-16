package composition;

import composition.wrappers.Demo;
import composition.wrappers.Functions;

import static composition.wrappers.Compose.compose;
import static java.lang.System.out;

public class Composition {
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
            Functions.f1(Demo::plus1)
                .andThen(Demo::plus1)
                .andThen(Demo::plus1)
                .andThen(Demo::plus1)
                .andThen(Demo::plus1)
                .apply(0));
    }
}
