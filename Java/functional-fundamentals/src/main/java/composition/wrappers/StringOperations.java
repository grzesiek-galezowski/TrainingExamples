package composition.wrappers;

import io.vavr.Function1;

import static composition.wrappers.Functions.f1;

public class StringOperations {
    public static Function1<String, String> append(String ma) {
        return f1(s -> s + ma);
    }

    public static Function1<String, String> firstNElements(Integer idx) {
        return f1(s -> s.substring(0, idx));
    }

    public static Function1<String, String> replace(String arg1, String arg2) {
        return f1(s -> s.replace(arg1, arg2));
    }

    public static String firstN(int i, String rplc) {
        return rplc.substring(0,i);

    }

    public static String apnd(String s2, String s1) {
        return s1 + s2;
    }

    public static String rplc(String pattern, String newText, String s1) {
        return s1.replace(pattern, newText);
    }
}
