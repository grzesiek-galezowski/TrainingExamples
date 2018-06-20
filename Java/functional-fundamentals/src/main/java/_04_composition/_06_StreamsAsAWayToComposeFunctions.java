package _04_composition;

import java.util.Arrays;
import java.util.List;

import static _04_composition.wrappers.Functions.f0;
import static _04_composition.wrappers.Text.F;
import static java.util.stream.Collectors.toList;

public class _06_StreamsAsAWayToComposeFunctions {

    public static void main(String[] args) {


        List<String> strings =
            Arrays.asList("Ewa", "Kot", "Hubert", "Ala", "Julka").stream()
            .map(F.append(" "))
            .map(F.append("ma"))
            .map(F.append(" "))
            .map(F.append("psa"))
            .map(F.append("."))
            .map(F.replace("psa", "kota"))
            .collect(toList()); //reduce

        String string = f0(() -> "Ewa")
            .andThen(F.append(" "))
            .andThen(F.append("ma"))
            .andThen(F.append(" "))
            .andThen(F.append("psa"))
            .andThen(F.append("."))
            .andThen(F.replace("psa", "kota"))
            .apply(); //reduce

    }
}

