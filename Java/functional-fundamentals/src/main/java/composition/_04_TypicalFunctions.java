package composition;

import composition.wrappers.Functions;
import composition.wrappers.MyStringFunction;
import composition.wrappers.StringOperations;
import lombok.val;

import java.util.function.Function;

import static composition.wrappers.Functions.*;
import static composition.wrappers.Functions.f3;
import static composition.wrappers.StringOperations.*;

public class _04_TypicalFunctions {

    public static void main(String[] args) {

        String str = "Ewa";

        //1
        val result = firstN(4, rplc("psa", "kota", apnd(".", apnd("psa", apnd(" ", apnd("ma", apnd(" ", str)))))));


        // 2
        Function<String, String> compose = compose((String s) -> apnd(" ", s),
            compose(s -> apnd("ma", s),
                compose(s -> apnd(" ", s),
                    compose(s -> apnd("psa", s),
                        compose(s -> apnd(".", s),
                            compose(s -> rplc("psa", "kota", s),
                                s -> firstN(4, s)
                            )
                        )
                    )
                )
            )
        );

        // 3
        Function<String, String> stringFunction
            = Functions.<String, String>f1(s -> apnd(" ", s))
            .andThen(s -> apnd("ma", s))
            .andThen(s -> apnd(" ", s))
            .andThen(s -> apnd("psa", s))
            .andThen(s -> apnd(".", s))
            .andThen(s -> rplc("psa", "kota", s))
            .andThen(s -> firstN(4, s));

        Function<String, String> stringFunction2
            = f1(append(" "))
            .andThen(append("ma"))
            .andThen(append(" "))
            .andThen(append("psa"))
            .andThen(append("."))
            .andThen(replace("psa", "kota"))
            .andThen(firstNElements(4));

        MyStringFunction myStringFunction1 = mf(s -> s + " ")
            >> append("ma")
            >> append(" ")
            >> append("psa")
            >> append(".")
            >> append(".")
            >> replace("psa", "kota")
            >> firstNElements(4);

        //partial application 1
        MyStringFunction myStringFunction2 = mf(s -> s + " ")
            >> f2(StringOperations::apnd).apply("ma")
            >> f2(StringOperations::apnd).apply(" ")
            >> f2(StringOperations::apnd).apply("psa")
            >> f2(StringOperations::apnd).apply(".")
            >> f2(StringOperations::apnd).apply(".")
            >> f3(StringOperations::rplc).apply("psa").apply("kota")
            >> f2(StringOperations::firstN).apply(4);

        //partial application 2
        MyStringFunction myStringFunction3 = mf(s -> s + " ")
            >> f2(StringOperations::apnd).apply("ma")
            >> f2(StringOperations::apnd).apply(" ")
            >> f2(StringOperations::apnd).apply("psa")
            >> f2(StringOperations::apnd).apply(".")
            >> f2(StringOperations::apnd).apply(".")
            >> f3(StringOperations::rplc).apply("psa").apply("kota")
            >> f2(StringOperations::firstN).apply(4);

        System.out.println(result);
    }

    public static <T, U, V> Function<T, V> compose(
        Function<T, U> f,
        Function<U, V> g) {

        return instance -> g.apply(f.apply(instance));
    }

}
