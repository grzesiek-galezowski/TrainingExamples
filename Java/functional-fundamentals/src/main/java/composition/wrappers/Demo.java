package composition.wrappers;

public class Demo {
    public static int plus1(int input) {
        return input + 1;
    }

    public static int minus(int a, int b) {
        return a - b;
    }

    public static String asString(int a) {
        return String.valueOf(a);
    }
}
