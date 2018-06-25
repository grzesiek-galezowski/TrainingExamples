package _01_forget_about_before_and_after;

import org.testng.annotations.AfterClass;
import org.testng.annotations.AfterMethod;
import org.testng.annotations.AfterSuite;
import org.testng.annotations.AfterTest;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.BeforeMethod;
import org.testng.annotations.BeforeSuite;
import org.testng.annotations.BeforeTest;
import org.testng.annotations.Test;

public class ExampleSpecification {

    @BeforeMethod
    public void beforeMethod() {
        System.out.println("Before Method");
    }

    @BeforeTest
    public void beforeTest() {
        System.out.println("Before Test");
    }

    @BeforeSuite
    public void beforeSuite() {
        System.out.println("Before Suite");
    }

    @BeforeClass
    public void beforeClass() {
        System.out.println("Before Class");
    }

    @Test
    public void shouldWhatever1() {
        System.out.println("shouldWhatever1");
    }

    @Test
    public void shouldWhatever2() {
        System.out.println("shouldWhatever2");
    }

    @AfterMethod
    public void afterMethod() {
        System.out.println("After Method");
    }

    @AfterTest
    public void afterTest() {
        System.out.println("After Test");
    }

    @AfterSuite
    public void afterSuite() {
        System.out.println("After Suite");
    }

    @AfterClass
    public void afterClass() {
        System.out.println("After Class");
    }

}
