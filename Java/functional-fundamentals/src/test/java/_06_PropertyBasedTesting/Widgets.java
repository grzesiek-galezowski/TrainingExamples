package _06_PropertyBasedTesting;

import org.quicktheories.core.Gen;
import org.quicktheories.core.RandomnessSource;

import static org.quicktheories.generators.SourceDSL.integers;

class Widgets implements Gen<Widget> {

    private final Gen<Integer> args1;
    private final Gen<Integer> args2;

    Widgets(Gen<Integer> args1, Gen<Integer> args2) {
        this.args1 = args1;
        this.args2 = args2;
    }

    @Override
    public Widget generate(RandomnessSource in) {
        return new Widget(
            args1.generate(in),
            args2.generate(in));
    }

    public static Widgets withPositiveSize() {
        return new Widgets(
            integers().allPositive(),
            integers().allPositive());
    }

    public static Widgets withNonPositiveSize() {
        return new Widgets(
            integers().between(Integer.MIN_VALUE, 0),
            integers().between(Integer.MIN_VALUE, 0));
    }

}
