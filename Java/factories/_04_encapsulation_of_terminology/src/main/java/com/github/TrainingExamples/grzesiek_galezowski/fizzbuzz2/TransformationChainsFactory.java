package com.github.TrainingExamples.grzesiek_galezowski.fizzbuzz2;

@SuppressWarnings("unused")
public class TransformationChainsFactory {
    public TransformationChain createFizzBuzz() {
        return new RuleBasedTransformationChain(
            createFizzBuzzRule(),
            new Fizz(),
            new Buzz()
        );
    }

    private Join createFizzBuzzRule() {
        return new Join(new Fizz(), new Buzz());
    }
}
