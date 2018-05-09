package analysis;

import java.util.Arrays;

public class _02_Streams {

    public static void main(String[] args) {

        final String primerPhrase =
            Arrays.asList("Ewa", "Kot", "Hubert", "Ala", "Julka").stream()
                .map(s -> s + " ")      // Stream.of("Ewa ", "Kot ", "Hubert ", "Ala ", "Julka ")
                .map(s -> s + "ma")     // Stream.of("Ewa ma", "Kot ma", "Hubert ma", "Ala ma", "Julka ma")
                .map(s -> s + " ")      // Stream.of("Ewa ma ", "Kot ma ", "Hubert ma ", "Ala ma ", "Julka ma ")
                .map(s -> s + "psa")    // Stream.of("Ewa ma psa", "Kot ma psa", "Hubert ma psa", "Ala ma psa", "Julka ma psa")
                .map(s -> s + ".")      // Stream.of("Ewa ma psa.", "Kot ma psa.", "Hubert ma psa.", "Ala ma psa.", "Julka ma psa.")
                .skip(1)                // Stream.of("Kot ma psa.", "Hubert ma psa.", "Ala ma psa.", "Julka ma psa.")
                .sorted()               // Stream.of("Ala ma psa.", "Hubert ma psa.", "Julka ma psa.", "Kot ma psa.")
                .map(s -> s.replace("psa", "kota")) // Stream.of("Ala ma kota.", "Hubert ma kota.", "Julka ma kota.", "Kot ma kota.")
                .limit(3)               // Stream.of("Ala ma kota.", "Hubert ma kota.", "Julka ma kota.")
                .findFirst()            // "Ala ma kota."
                .orElse("Ups! Zapomniałem elementarza.");

    }

}

