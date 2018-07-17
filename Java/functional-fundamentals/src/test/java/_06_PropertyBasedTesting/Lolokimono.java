package _06_PropertyBasedTesting;

import org.junit.Test;

import static org.quicktheories.QuickTheory.qt;
import static org.quicktheories.generators.Generate.constant;
import static org.quicktheories.generators.Generate.range;
import static org.quicktheories.generators.SourceDSL.integers;
import static org.quicktheories.generators.SourceDSL.lists;
import static org.quicktheories.generators.SourceDSL.strings;

public class Lolokimono {
    @Test
    public void addingTwoPositiveIntegersAlwaysGivesAPositiveInteger(){
        qt()
            .forAll(integers().allPositive()
                , integers().allPositive())
            .check((i,j) -> i + j > 0);
    }

    @Test
    public void someProperty() {
        qt()
            .forAll(range(1, 102), constant(7))
            .check((i,c) -> i + c >= 7);
    }

    @Test
    public void someTheoryOrOther(){
        qt()
            .forAll(integers().allPositive()
                , strings().basicLatinAlphabet().ofLengthBetween(0, 10)
                , lists().of(integers().all()).ofSize(42))
            .check((i,s,l) -> l.contains(i) && s.equals(""));
    }

    @Test
    public void someTheoryOrOther1(){
        qt()
            .forAll(integers().allPositive()
                , strings().basicLatinAlphabet().ofLengthBetween(0, 10)
                , lists().of(integers().all()).ofSize(42))
            .assuming((i,s,l) -> s.contains(i.toString())) // <-- an assumption
            .check((i,s,l) -> l.contains(i) && s.contains(i.toString()));
    }

    @Test
    public void badUseOfAssumptions() {
        qt()
            .forAll(integers().allPositive())
            .assuming(i -> i < 30000)
            .check( i -> i < 3000);
    }

    @Test
    public void someTheoryOrOther3(){
        qt()
            .forAll(integers().allPositive()
                , integers().allPositive())
            .as( (width,height) -> new Widget(width,height) ) // <-- convert to our own type here
            .check( widget -> widget.isValid());
    }
}
