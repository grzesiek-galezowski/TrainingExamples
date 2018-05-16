package composition;

import java.util.Arrays;
import java.util.function.Function;

public class _02_Streams {

    public static void main(String[] args) {

        final String primerPhrase =
            Arrays.asList("Ewa", "Kot", "Hubert", "Ala", "Julka").stream()
                .map(add(" "))
                .map(add("ma"))
                .map(add(" "))
                .map(add("psa"))
                .map(add("."))
                .skip(1)
                .sorted()
                .map(replace("psa", "kota"))
                .limit(3)
                .findFirst()
                .orElse("Ups! Zapomniałem elementarza.");

    }

    private static Function<String, String> replace(String psa, String kota) {
        return s -> s.replace(psa, kota);
    }

    private static Function<String, String> add(String s1) {
        return s -> s + s1;
    }

}

