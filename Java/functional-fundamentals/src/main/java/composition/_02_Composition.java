package composition;

import composition.wrappers.Demo;

import static composition.wrappers.Compose.compose;
import static composition.wrappers.Functions.f1;
import static java.lang.System.out;

public class _02_Composition {
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
            f1(Demo::plus1)
                .andThen(Demo::plus1)
                .andThen(Demo::plus1)
                .andThen(Demo::plus1)
                .andThen(Demo::plus1)
                .apply(0));

        //what's the difference?
        out.println(
            f1(Demo::plus1)
                .compose(Demo::plus1)
                .compose(Demo::plus1)
                .compose(Demo::plus1)
                .compose(Demo::plus1)
                .apply(0));
    }
}
