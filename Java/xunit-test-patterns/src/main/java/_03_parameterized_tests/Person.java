package _03_parameterized_tests;

public class Person {
    private int age;

    public Person(int age) {
        this.age = age;
    }

    public boolean isAdult() {
        return age >= 18;

    }
}
