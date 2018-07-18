package _06_PropertyBasedTesting;

import org.quicktheories.core.Gen;
import org.quicktheories.core.RandomnessSource;

public class MyRectangleGen implements Gen<MyRectangle> {
    private final Gen<Integer> widthGen;
    private final Gen<Integer> heightGen;

    public MyRectangleGen(Gen<Integer> widthGen, Gen<Integer> heightGen) {
        this.widthGen = widthGen;
        this.heightGen = heightGen;
    }

    @Override
    public MyRectangle generate(RandomnessSource in) {
        return new MyRectangle(widthGen.generate(in), heightGen.generate(in));
    }
}
