package problems;

import autofixture.publicinterface.Any;
import lombok.val;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.Test;

import java.util.ArrayList;
import java.util.List;

import static org.assertj.core.api.Assertions.assertThat;

public class _01_JointFixtures {
    private List<Integer> emptyList;
    private ArrayList<Integer> listWithElement;
    private Integer anyNumber;

    @BeforeMethod
    public void setUp() {
        //A
        emptyList = new ArrayList<>();

        //B
        anyNumber = Any.intValue();
        listWithElement = new ArrayList<>();
        listWithElement.add(anyNumber);
    }

    @Test //A
    public void ShouldHaveElementCountOf0AfterBeingCreated() {
        assertThat(emptyList.size()).isEqualTo(0);
    }

    @Test //A
    public void ShouldBeEmptyAfterBeingCreated() {
        assertThat(emptyList.isEmpty()).isTrue();
    }

    @Test //B
    public void ShouldNotContainElementRemovedFromIt() {
        listWithElement.remove(anyNumber);
        assertThat(listWithElement.contains(anyNumber)).isFalse();
    }

    @Test //B
    public void ShouldIncrementElementCountEachTimeTheSameElementIsAdded() {
        val previousCount = listWithElement.size();
        listWithElement.add(anyNumber);
        assertThat(listWithElement.size()).isEqualTo(previousCount + 1);
    }
}
