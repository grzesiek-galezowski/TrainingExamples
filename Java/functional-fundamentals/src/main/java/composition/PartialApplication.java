package composition;

import io.vavr.Function1;
import io.vavr.Function3;

import java.math.BigInteger;
import java.util.function.BiFunction;
import java.util.function.Function;
import java.util.function.Supplier;

public class PartialApplication {

    public void main(String[] args) {

        BigInteger i1 = 123;
        BigInteger i2 = 123;
        System.out.println(i1 < i2);
    }


    public static <T1, TReturn>
    Supplier<TReturn> partial(
        Function<T1, TReturn> f,
        T1 arg) {

        return () -> f.apply(arg);
    }

    public static <T1, T2, TReturn>
    Function<T2, TReturn> partial1st(
        BiFunction<T1, T2, TReturn> f,
        T1 first) {

        return (a) -> f.apply(first, a);
    }

    public static <T1, T2, TReturn>
    Function<T1, TReturn> partial2nd(
        BiFunction<T1, T2, TReturn> f,
        T2 second) {

        return (a) -> f.apply(a, second);
    }

    public static <T1, T2, T3, TReturn>
    Function1<T3, TReturn> partial21st(
        Function3<T1, T2, T3, TReturn> f,
        T1 first,
        T2 second) {

        return (a) -> f.apply(first, second, a);
    }



}
