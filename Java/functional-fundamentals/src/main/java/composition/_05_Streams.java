package composition;

import java.util.Arrays;
import java.util.List;
import java.util.function.Function;

import static composition.wrappers.Functions.f0;
import static java.util.stream.Collectors.toList;

public class _05_Streams {

    public static void main(String[] args) {


        List<String> strings =
            Arrays.asList("Ewa", "Kot", "Hubert", "Ala", "Julka").stream()
            .map(add(" "))
            .map(add("ma"))
            .map(add(" "))
            .map(add("psa"))
            .map(add("."))
            .map(replace("psa", "kota"))
            .collect(toList()); //reduce

        String string = f0(() -> "Ewa")
            .andThen(add(" "))
            .andThen(add("ma"))
            .andThen(add(" "))
            .andThen(add("psa"))
            .andThen(add("."))
            .andThen(replace("psa", "kota"))
            .apply(); //reduce

    }

    private static Function<String, String> replace(String psa, String kota) {
        return s -> s.replace(psa, kota);
    }

    private static Function<String, String> add(String s1) {
        return s -> s + s1;
    }

}

