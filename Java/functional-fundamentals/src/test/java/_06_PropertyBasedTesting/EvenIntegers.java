package _06_PropertyBasedTesting;

import org.quicktheories.core.Gen;
import org.quicktheories.core.RandomnessSource;
import org.quicktheories.generators.IntegersDSL;

public class EvenIntegers implements Gen<Integer> {
    private Gen<Integer> integerGen;

    public EvenIntegers(Gen<Integer> integerGen) {
        this.integerGen = integerGen;
    }

    public static EvenIntegers even(IntegersDSL integers) {
        return new EvenIntegers(integers.all());
    }

    @Override
    public Integer generate(RandomnessSource in) {
        Integer num = integerGen.generate(in);
        if(num % 2 == 0) {
            return num;
        } else {
            return num + 1;
        }
    }
}
