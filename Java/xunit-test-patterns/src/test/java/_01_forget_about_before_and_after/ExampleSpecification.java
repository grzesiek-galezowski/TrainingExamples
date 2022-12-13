package _01_forget_about_before_and_after;

import org.junit.jupiter.api.AfterAll;
import org.junit.jupiter.api.AfterEach;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

public class ExampleSpecification {

    @BeforeEach
    public void beforeMethod() {
        System.out.println("Before Method");
    }

    @BeforeAll
    public static void beforeSuite() {
        System.out.println("Before Suite");
    }

    @Test
    public void shouldWhatever1() {
        System.out.println("shouldWhatever1");
    }

    @Test
    public void shouldWhatever2() {
        System.out.println("shouldWhatever2");
    }

    @AfterEach
    public void afterMethod() {
        System.out.println("After Method");
    }

    @AfterAll
    public static void afterSuite() {
        System.out.println("After Suite");
    }
}
