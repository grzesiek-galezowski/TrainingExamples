package com.github.TrainingExamples.grzesiek_galezowski.fizzbuzz2;

import java.util.Arrays;
import java.util.function.Function;
import java.util.stream.Collector;

import static java.util.stream.Collectors.joining;

public class Join implements Rule {
    private Rule[] rules;

    public Join(Rule... rules) {
        this.rules = rules;
    }

    @Override
    public boolean appliesTo(int n) {
        return Arrays.stream(rules)
            .allMatch(r -> r.appliesTo(n));
    }

    @Override
    public String convert(int n) {
        return Arrays.stream(rules)
            .map(toResult(n))
            .collect(toConcatenatedString());
    }

    private Collector<CharSequence, ?, String> toConcatenatedString() {
        return joining("");
    }

    private Function<Rule, String> toResult(int n) {
        return r -> r.convert(n);
    }
}
