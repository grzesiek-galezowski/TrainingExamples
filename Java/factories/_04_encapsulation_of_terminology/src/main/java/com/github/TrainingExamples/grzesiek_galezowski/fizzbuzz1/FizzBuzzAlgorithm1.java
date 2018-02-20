package com.github.TrainingExamples.grzesiek_galezowski.fizzbuzz1;

public class FizzBuzzAlgorithm1 {

    public String buzzIt(int n) {
        if (n % 3 == 0 && n % 5 == 0) {
            return "FizzBuzz";
        }
        if (n % 3 == 0) {
            return "Fizz";
        }
        if (n % 5 == 0) {
            return "Buzz";
        }
        return String.valueOf(n);
    }
}
