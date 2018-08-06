package _06_PropertyBasedTesting;

import org.quicktheories.core.Gen;
import org.quicktheories.core.RandomnessSource;
import org.quicktheories.generators.IntegersDSL;

public class OddIntegers implements Gen<Integer> {
    private Gen<Integer> integerGen;

    public OddIntegers(Gen<Integer> integerGen) {
        this.integerGen = integerGen;
    }

    public static OddIntegers odd(IntegersDSL integers) {
        return new OddIntegers(integers.all());
    }

    @Override
    public Integer generate(RandomnessSource in) {
        Integer num = integerGen.generate(in);
        if(num % 2 == 0) {
            return num + 1;
        } else {
            return num;
        }
    }
}
