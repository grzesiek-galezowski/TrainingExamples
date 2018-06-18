package composition;

import java.util.Arrays;
import java.util.List;

import static composition.wrappers.Functions.f0;
import static composition.wrappers.Text.appendF;
import static composition.wrappers.Text.replaceF;
import static java.util.stream.Collectors.toList;

public class _06_StreamsAsAWayToComposeFunctions {

    public static void main(String[] args) {


        List<String> strings =
            Arrays.asList("Ewa", "Kot", "Hubert", "Ala", "Julka").stream()
            .map(appendF(" "))
            .map(appendF("ma"))
            .map(appendF(" "))
            .map(appendF("psa"))
            .map(appendF("."))
            .map(replaceF("psa", "kota"))
            .collect(toList()); //reduce

        String string = f0(() -> "Ewa")
            .andThen(appendF(" "))
            .andThen(appendF("ma"))
            .andThen(appendF(" "))
            .andThen(appendF("psa"))
            .andThen(appendF("."))
            .andThen(replaceF("psa", "kota"))
            .apply(); //reduce

    }

    /*
    private static Function<String, String> replace(String psa, String kota) {
        return s -> s.replace(psa, kota);
    }

    private static Function<String, String> add(String s1) {
        return s -> s + s1;
    }*/

}

