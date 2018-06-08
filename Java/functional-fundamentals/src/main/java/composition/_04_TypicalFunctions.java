package composition;

import composition.wrappers.Functions;
import composition.wrappers.MyStringFunction;
import composition.wrappers.Text;
import lombok.val;

import java.util.function.Function;

import static composition.wrappers.Functions.*;
import static composition.wrappers.Functions.f3;
import static composition.wrappers.Text.*;

public class _04_TypicalFunctions {

    public static void main(String[] args) {

        String str = "Ewa";

        //1
        val result = firstN(4, replace("psa", "kota", append(".", append("psa", append(" ", append("ma", append(" ", str)))))));


        // 2
        Function<String, String> compose = compose((String s) -> append(" ", s),
            compose(s -> append("ma", s),
                compose(s -> append(" ", s),
                    compose(s -> append("psa", s),
                        compose(s -> append(".", s),
                            compose(s -> replace("psa", "kota", s),
                                s -> firstN(4, s)
                            )
                        )
                    )
                )
            )
        );

        // 3
        Function<String, String> stringFunction
            = Functions.<String, String>f1(s -> append(" ", s))
            .andThen(s -> append("ma", s))
            .andThen(s -> append(" ", s))
            .andThen(s -> append("psa", s))
            .andThen(s -> append(".", s))
            .andThen(s -> replace("psa", "kota", s))
            .andThen(s -> firstN(4, s));

        Function<String, String> stringFunction2
            = f1(appendF(" "))
            .andThen(appendF("ma"))
            .andThen(appendF(" "))
            .andThen(appendF("psa"))
            .andThen(appendF("."))
            .andThen(replaceF("psa", "kota"))
            .andThen(firstNElementsF(4));

        MyStringFunction myStringFunction1 = mf(s -> s + " ")
            >> appendF("ma")
            >> appendF(" ")
            >> appendF("psa")
            >> appendF(".")
            >> appendF(".")
            >> replaceF("psa", "kota")
            >> firstNElementsF(4);

        //partial application 1
        MyStringFunction myStringFunction2 = mf(s -> s + " ")
            >> f2(Text::append).apply("ma")
            >> f2(Text::append).apply(" ")
            >> f2(Text::append).apply("psa")
            >> f2(Text::append).apply(".")
            >> f2(Text::append).apply(".")
            >> f3(Text::replace).apply("psa").apply("kota")
            >> f2(Text::firstN).apply(10);
        System.out.println(myStringFunction2.apply("["));

        //partial application 2
        MyStringFunction myStringFunction3 = mf(s -> s + " ")
            >> f2(Text::append).apply("ma")
            >> f2(Text::append).apply(" ")
            >> f2(Text::append).apply("psa")
            >> f2(Text::append).apply(".")
            >> f2(Text::append).apply(".")
            >> f3(Text::replace).apply("psa").apply("kota")
            >> f2(Text::firstN).apply(4);

        System.out.println(result);
    }

    public static <T, U, V> Function<T, V> compose(
        Function<T, U> f,
        Function<U, V> g) {

        return instance -> g.apply(f.apply(instance));
    }

}
