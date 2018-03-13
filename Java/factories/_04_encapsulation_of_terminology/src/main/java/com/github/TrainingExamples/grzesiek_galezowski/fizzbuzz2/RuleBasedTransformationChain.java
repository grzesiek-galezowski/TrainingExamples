package com.github.TrainingExamples.grzesiek_galezowski.fizzbuzz2;

import java.security.InvalidAlgorithmParameterException;

public class RuleBasedTransformationChain implements TransformationChain {

    private Rule[] rules;

    public RuleBasedTransformationChain(Rule... rules) {
        this.rules = rules;
    }

    @Override
    public String convert(int n) throws InvalidAlgorithmParameterException {
        for(Rule r : rules) {
            if(r.appliesTo(n)) {
                return r.convert(n);
            }
        }
        throw new InvalidAlgorithmParameterException();
    }
}
