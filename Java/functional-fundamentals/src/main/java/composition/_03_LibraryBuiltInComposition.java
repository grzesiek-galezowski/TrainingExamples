package composition;

import composition.wrappers.Arithmetics;

import static composition.wrappers.Functions.f1;
import static java.lang.System.out;

public class _03_LibraryBuiltInComposition {
    public static void main(String[] args) {

        //BUILT-IN COMPOSITION (VAVR) - andThen()
        out.println(
            f1(Arithmetics::plus1)
                .andThen(Arithmetics::plus1)
                .andThen(Arithmetics::plus1)
                .andThen(Arithmetics::plus1)
                .andThen(Arithmetics::plus1)
                .apply(0));

        //BUILT-IN COMPOSITION (VAVR) - compose()
        out.println(
            f1(Arithmetics::plus1)
                .compose(Arithmetics::plus1)
                .compose(Arithmetics::plus1)
                .compose(Arithmetics::plus1)
                .compose(Arithmetics::plus1)
                .apply(0));
    }
}
