package analysis;

import java.util.Arrays;

public class _02_Streams {

    public static void main(String[] args) {

        final String primerPhrase =
            Arrays.asList("Ewa", "Kot", "Hubert", "Ala", "Julka").stream()
                .map(s -> s + " ")      // Arrays.asList("Ewa ", "Kot ", "Hubert ", "Ala ", "Julka ").stream()
                .map(s -> s + "ma")     // Arrays.asList("Ewa ma", "Kot ma", "Hubert ma", "Ala ma", "Julka ma").stream()
                .map(s -> s + " ")      // Arrays.asList("Ewa ma ", "Kot ma ", "Hubert ma ", "Ala ma ", "Julka ma ").stream()
                .map(s -> s + "psa")    // Arrays.asList("Ewa ma psa", "Kot ma psa", "Hubert ma psa", "Ala ma psa", "Julka ma psa").stream()
                .map(s -> s + ".")      // Arrays.asList("Ewa ma psa.", "Kot ma psa.", "Hubert ma psa.", "Ala ma psa.", "Julka ma psa.").stream()
                .skip(1)                // Arrays.asList("Kot ma psa.", "Hubert ma psa.", "Ala ma psa.", "Julka ma psa.").stream()
                .sorted()               // Arrays.asList("Ala ma psa.", "Hubert ma psa.", "Julka ma psa.", "Kot ma psa.").stream()
                .map(s -> s.replace("psa", "kota")) // Arrays.asList("Ala ma kota.", "Hubert ma kota.", "Julka ma kota.", "Kot ma kota.").stream()
                .limit(3)               // Arrays.asList("Ala ma kota.", "Hubert ma kota.", "Julka ma kota.").stream()
                .findFirst()            // "Ala ma kota."
                .orElse("Ups! Zapomniałem elementarza.");

    }

}

