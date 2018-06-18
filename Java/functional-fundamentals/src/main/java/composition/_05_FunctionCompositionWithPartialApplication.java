package composition;

import composition.wrappers.Functions;
import composition.wrappers.MyStringFunction;
import composition.wrappers.Text;
import lombok.val;

import java.util.function.Function;

import static composition.wrappers.Compose.compose;
import static composition.wrappers.Functions.f1;
import static composition.wrappers.Functions.f2;
import static composition.wrappers.Functions.f3;
import static composition.wrappers.Functions.myFun;
import static composition.wrappers.Text.append;
import static composition.wrappers.Text.appendF;
import static composition.wrappers.Text.firstN;
import static composition.wrappers.Text.firstNElementsF;
import static composition.wrappers.Text.replace;
import static composition.wrappers.Text.replaceF;

public class _05_FunctionCompositionWithPartialApplication {

    public static void main(String[] args) {

        String str = "Ewa";

        // NO COMPOSITION
        val result = firstN(4, replace("psa", "kota", append(".", append("psa", append(" ", append("ma", append(" ", str)))))));


        // MANUAL COMPOSITION
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

        // LIBRARY COMMPOSITION
        Function<String, String> stringFunction
            = Functions.<String, String>f1(s -> append(" ", s))
            .andThen(s -> append("ma", s))
            .andThen(s -> append(" ", s))
            .andThen(s -> append("psa", s))
            .andThen(s -> append(".", s))
            .andThen(s -> replace("psa", "kota", s))
            .andThen(s -> firstN(4, s));

        // LIBRARY COMMPOSITION
        // + DECLARATIVE APPROACH WITH HIGHER ORDER FUNCTIONS
        Function<String, String> stringFunction2
            = f1(appendF(" "))
            .andThen(appendF("ma"))
            .andThen(appendF(" "))
            .andThen(appendF("psa"))
            .andThen(appendF("."))
            .andThen(replaceF("psa", "kota"))
            .andThen(firstNElementsF(4));

        // HOW NON-OO LANGUAGES DO IT... OPERATOR OVERLOADING
        MyStringFunction myStringFunction1 = myFun(s -> s + " ")
            >> appendF("ma")
            >> appendF(" ")
            >> appendF("psa")
            >> appendF(".")
            >> appendF(".")
            >> replaceF("psa", "kota")
            >> firstNElementsF(4);

        // OPERATORS WITH PARTIAL APPLICATION
        MyStringFunction myStringFunction2 = myFun(s -> s + " ")
            >> f2(Text::append).apply("ma")
            >> f2(Text::append).apply(" ")
            >> f2(Text::append).apply("psa")
            >> f2(Text::append).apply(".")
            >> f2(Text::append).apply(".")
            >> f3(Text::replace).apply("psa").apply("kota")
            >> f2(Text::firstN).apply(10);
        System.out.println(myStringFunction2.apply("["));

    }

}
