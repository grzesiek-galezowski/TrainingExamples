package com.github.TrainingExamples.grzesiek_galezowski.fizzbuzz2;

@SuppressWarnings("unused")
public class TransformationChainsFactory {
    public TransformationChain createFizzBuzz() {
        return new RuleBasedTransformationChain(
            new Join(new Fizz(), new Buzz()),
            new Fizz(),
            new Buzz()
        );
    }
}
