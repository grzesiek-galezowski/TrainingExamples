package _04_composition.wrappers;

import io.vavr.Function1;

public class MyStringFunction {

    private Function1<String, String> f;

    public MyStringFunction(Function1<String, String> stringStringFunction) {
        f = stringStringFunction;
    }

    public MyStringFunction shiftRight(Function1<String, String> after) {
        return new MyStringFunction(f.andThen(after));
    }

    public String apply(String arg) {
        return f.apply(arg);
    }
}