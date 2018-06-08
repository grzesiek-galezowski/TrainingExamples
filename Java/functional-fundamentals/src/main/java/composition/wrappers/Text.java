package composition.wrappers;

import io.vavr.Function1;

import static composition.wrappers.Functions.f1;

public class Text {
    public static Function1<String, String> appendF(String ma) {
        return f1(s -> s + ma);
    }

    public static Function1<String, String> firstNElementsF(Integer idx) {
        return f1(s -> s.substring(0, idx));
    }

    public static Function1<String, String> replaceF(String arg1, String arg2) {
        return f1(s -> s.replace(arg1, arg2));
    }

    public static String firstN(int i, String rplc) {
        return rplc.substring(0,i);

    }

    public static String append(String s2, String s1) {
        return s1 + s2;
    }

    public static String replace(String pattern, String newText, String s1) {
        return s1.replace(pattern, newText);
    }
}
