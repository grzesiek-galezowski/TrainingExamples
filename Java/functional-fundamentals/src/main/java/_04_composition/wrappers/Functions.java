package _04_composition.wrappers;

import io.vavr.Function0;
import io.vavr.Function1;
import io.vavr.Function2;
import io.vavr.Function3;

public class Functions {
    public static <U> Function0<U> f0(
        Function0<U> f) {
        return f;
    }
    public static <T, U> Function1<T,U> f1(
        Function1<T, U> f) {
        return f;
    }

    public static <T, U> Function1<T, U> s(
        Function1<T, U> f) {
        return f;
    }

    public static <T, U, V> Function2<T, U, V> f2(
        Function2<T, U, V> f) {
        return f;
    }

    public static Function3<String, String, String, String> f3(
        Function3<String, String, String, String> f) {
        return f;
    }

    public static MyStringFunction myFun(
        Function1<String, String> f) {
        return new MyStringFunction(f);
    }
}
