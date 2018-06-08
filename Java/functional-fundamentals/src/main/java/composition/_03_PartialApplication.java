package composition;

import composition.wrappers.Demo;
import io.vavr.Function0;
import io.vavr.Function1;
import io.vavr.Function2;
import io.vavr.Function3;
import lombok.val;

import java.util.function.Function;

import static composition.wrappers.Functions.f2;
import static java.lang.System.out;
import static java.util.Arrays.asList;
import static java.util.stream.Collectors.toList;

public class _03_PartialApplication {

    public static void main(String[] args) {
        val onePlusA = bind1st(Demo::minus, 1);
        out.println(onePlusA.apply(2)); //=> -1


        val aPlusOne = bindLast(Demo::minus, 1);
        out.println(aPlusOne.apply(2)); //=> 1

        val transformed
            = asList(1, 2, 3).stream()
            .map(aPlusOne)
            .collect(toList());

        val transformed2
            = asList(1, 2, 3).stream()
            .map(bindLast(Demo::minus, 1))
            .collect(toList());

        val transformed3
            = asList(1, 2, 3).stream()
            .map(f2(Demo::minus).apply(1))
            .collect(toList());

    }

    public static <T1, TReturn>
    Function0<TReturn> bind(
        Function<T1, TReturn> f,
        T1 arg) {

        return () -> f.apply(arg);
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

    public static <T1, T2, T3, TReturn>
    Function1<T3, TReturn> bind2First(
        Function3<T1, T2, T3, TReturn> f,
        T1 first,
        T2 second) {

        return (a) -> f.apply(first, second, a);
    }



}
