package _04_composition;

import _04_composition.wrappers.Arithmetics;
import io.vavr.Function1;
import io.vavr.Function2;
import lombok.val;

import static _04_composition.wrappers.Functions.f2;
import static java.util.Arrays.asList;
import static java.util.stream.Collectors.toList;

public class _04_BasicPartialApplication {

    public static void main(String[] args) {

        // PARTIALLY APPLYING THE 1ST ARG
        final Function1<Integer, Integer> onePlusA
            = bind1st(Arithmetics::minus, 1);

        //out.println(onePlusA.apply(2)); //=> -1

        // PARTIALLY APPLYING THE LAST ARG
        final Function1<Integer, Integer>
            aPlusOne = bindLast(Arithmetics::minus, 1);

        //out.println(aPlusOne.apply(2)); //=> 1

        // USING PARTIALLY APPLIED FUNCTION VARIABLES
        val transformed
            = asList(1, 2, 3).stream()
            .map(aPlusOne)
            .collect(toList());

        // USING INLINE PARTIAL APPLICATION OF 1ST ARG
        val transformed2
            = asList(1, 2, 3).stream()
            .map(bind1st(Arithmetics::minus, 1))
            .collect(toList());

        // USING LIBRARY-PROVIDED PARTIAL APPLICATION OF 1ST ARG
        val transformed3
            = asList(1, 2, 3).stream()
            .map(f2(Arithmetics::minus).apply(1))
            .collect(toList());
    }

    public static <T1, T2, TReturn>
    Function1<T2, TReturn> bind1st(
        Function2<T1, T2, TReturn> f,
        T1 first) {

        return (a) -> f.apply(first, a);
    }

    public static <T1, T2, TReturn>
    Function1<T1, TReturn> bindLast(
        Function2<T1, T2, TReturn> f,
        T2 second) {

        return (a) -> f.apply(a, second);
    }

}
