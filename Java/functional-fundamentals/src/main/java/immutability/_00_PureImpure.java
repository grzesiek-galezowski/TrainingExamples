package immutability;

import static java.lang.System.out;

public class _00_PureImpure {

    public static void main(String[] args) {
        out.println(pure(1));
        out.println(impure(1));
    }

    public static int pure(final int x) {
        return x + 1;
    }

    public static int impure(int x) {
        out.println("I have a side effect");
        return x + 1;
    }
}
