package _06_PropertyBasedTesting;

import org.junit.Test;
import org.quicktheories.core.Gen;

import static _06_PropertyBasedTesting.EvenIntegers.even;
import static _06_PropertyBasedTesting.OddIntegers.odd;
import static org.assertj.core.api.Assertions.assertThat;
import static org.quicktheories.QuickTheory.qt;
import static org.quicktheories.generators.Generate.constant;
import static org.quicktheories.generators.Generate.range;
import static org.quicktheories.generators.SourceDSL.integers;
import static org.quicktheories.generators.SourceDSL.lists;
import static org.quicktheories.generators.SourceDSL.strings;

public class QuickTheoriesExamples {
    @Test
    public void addingTwoPositiveIntegersAlwaysGivesAPositiveInteger() {

        qt()
            .forAll(
                integers().allPositive(),
                integers().allPositive())
            .check((i, j) -> i + j > 0);

        // todo show .assuming() with even numbers
        // todo show more than 2 input values
    }

    @Test
    public void addingNonNegativeTo7ProducesAtLeast7() {
        qt()
            .forAll(range(0, 102), constant(7))
            .check((i, c) -> i + c >= 7);

        //todo change 0 to -1 and see what happens
        //todo show qt().withFixedSeed() from the error report
    }

    @Test //shows nested generator
    public void doesNotMakeSenseButShowsNestedGenerators() {
        qt().forAll(
            integers().allPositive(),
            strings()
                .basicLatinAlphabet()
                .ofLengthBetween(0, 10),
            lists()
                .of(integers().all())
                .ofSize(42))
            .check((num, str, list) ->
                list.contains(num) && str.equals(""));
    }

    @Test
    public void badUseOfAssumptions() {
        qt().forAll(
            integers()
                .allPositive() //todo change to all() and see what happens
                .assuming(i -> i < 30000))
            .check(i -> i < 3000);
    }

    @Test //fails, too few examples
    public void sumOfEvenAndOddNumberIsAnOddNumber() {
        qt()
            .forAll(
                integers()
                    .all()
                    .assuming(a -> a % 2 == 0),
                integers().all()
                    .assuming(b -> b % 2 != 0))
            .check((even, odd) -> (even + odd) % 2 != 0);
    }

    @Test //an improvement over the last one
    public void sumOfEvenAndOddNumberIsAnOddNumber3() {
        qt()
            .forAll(
                new EvenIntegers(integers().all()),
                new OddIntegers(integers().all()))
            .check((even, odd) -> (even + odd) % 2 != 0);
    }

    @Test //an improvement over the last one
    public void sumOfEvenAndOddNumberIsAnOddNumber4() {
        qt()
            .forAll(
                even(integers()),
                odd(integers()))
            .check((even, odd) -> (even + odd) % 2 != 0);
    }

    @Test //shows checkAssert
    public void sumOfEvenAndOddNumberIsAnOddNumber5() {
        qt()
            .forAll(
                even(integers()),
                odd(integers()))
            .checkAssert(
                (even, odd) -> assertIsOdd(even + odd));
    }

    private void assertIsOdd(int i) {
        assertThat(i % 2).isNotEqualTo(0);
    }

    @Test
    public void showsUsingRealObjects() {
        qt()
            .forAll(
                integers().allPositive(),
                integers().allPositive())
            .as((width, height) -> new Widget(width, height)) // <-- convert to our own type here
            .check(widget -> widget.isValid());
    }

    @Test
    public void showsUsingRealObjects2() {
        qt()
            .forAll(
                integers().allPositive(),
                integers().allPositive())
            .asWithPrecursor((width, height) -> new Widget(width, height)) // <-- convert to our own type here
            .check((width, height, widget) -> widget.isValid());
    }

    @Test
    public void showsUsingRealObjectsWithCustomGenerators() {
        qt()
            .forAll(new Widgets(
                integers().allPositive(), //todo change to .all()
                integers().allPositive()))
            .check(widget -> widget.isValid());
    }

    @Test
    public void showsUsingRealObjectsWithCustomGeneratorsRefactored() {
        qt()
            .forAll(Widgets.withPositiveSize())
            .check(widget -> widget.isValid());
    }

    @Test
    public void showsUsingRealObjectsWithCustomGeneratorsRefactored2() {
        qt()
            .forAll(Widgets.withNonPositiveSize())
            .check(widget -> !widget.isValid());
    }

    @Test
    public void rectangleExample() {
        qt()
            .forAll(rectanglesWithDimensions(
                integers().allPositive(),
                integers().allPositive()))
            .checkAssert(rectangle ->
                assertThat(rectangle.getWidth() * rectangle.getHeight())
                .isEqualTo(rectangle.getField()));
    }

    private Gen<MyRectangle> rectanglesWithDimensions(Gen<Integer> widthGen, Gen<Integer> heightGen) {
        return new MyRectangleGen(widthGen, heightGen);
    }

}
