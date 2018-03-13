package com.github.TrainingExamples.grzesiek_galezowski.fizzbuzz2;

public interface Rule {
    boolean appliesTo(int n);

    String convert(int n);
}
