package com.github.TrainingExamples.grzesiek_galezowski.fizzbuzz2;

import java.security.InvalidAlgorithmParameterException;

public interface TransformationChain {
    String convert(int n) throws InvalidAlgorithmParameterException;
}
