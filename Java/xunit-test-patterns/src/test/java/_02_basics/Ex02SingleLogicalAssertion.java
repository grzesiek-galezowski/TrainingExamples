package _02_basics;

import autofixture.publicinterface.Any;
import lombok.val;
import lombok.var;
import org.junit.jupiter.api.Test;

import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex02SingleLogicalAssertion {

    //example 1: multiple runtime assertions
    @Test
    public void shouldLeaveUniqueItems() {
        //GIVEN
        val uniqueFilter = new UniqueFilter();

        //WHEN
        val result = uniqueFilter.applyTo(1, 2, 3, 3, 4, 3, 3);

        //THEN
        assertHasUniqueItems(result);
    }

    //example 2: multiple in-code assertions
    @Test
    public void shouldLeaveLast3UniqueItems() {
        //GIVEN
        val uniqueFilter = new UniqueFilter();

        //WHEN
        val result = uniqueFilter
            .apply3To(1, 2, 3, 3, 4, 3, 3);

        //THEN
        assertThat(result.get(0))
            .isNotEqualTo(result.get(1));
        assertThat(result.get(0))
            .isNotEqualTo(result.get(2));
        assertThat(result.get(1))
            .isNotEqualTo(result.get(2));
    }

    //violation
    @Test
    public void
        shouldReportItCanHandleStringWithLengthOf3ButNotOf4AndNotNullString() {
        //GIVEN
        var bufferSizeRule = new BufferSizeRule();

        //WHEN
        var resultForLengthOf3
            = bufferSizeRule.canHandle(
                Any.stringOfLength(3));

        //THEN
        assertThat(resultForLengthOf3).isTrue();

        //WHEN - again?
        var resultForLengthOf4
            = bufferSizeRule.canHandle(
                Any.stringOfLength(4));

        //THEN - again?
        assertThat(resultForLengthOf4).isFalse();

        //WHEN - again??
        var resultForNull = bufferSizeRule.canHandle(
            null);

        //THEN - again??
        assertThat(resultForNull).isFalse();
    }


    public static <T> void assertHasUniqueItems(List<T> list) {
        for (var i = 0; i < list.size(); i++) {
            for (var j = 0; j < list.size(); j++) {
                if (i != j) {
                    System.out.println("assertion!");
                    assertThat(list.get(i)).isNotEqualTo(list.get(j));
                }
            }
        }
    }
}
