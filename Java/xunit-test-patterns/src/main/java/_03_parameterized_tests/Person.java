package _03_parameterized_tests;

public class Person {
    public static final int ADULT_AGE = 18;
    private int age;

    public Person(int age) {
        this.age = age;
    }

    public boolean isAdult() {
        return age >= 18;

    }
}
