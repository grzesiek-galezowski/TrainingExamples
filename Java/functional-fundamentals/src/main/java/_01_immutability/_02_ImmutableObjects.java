package _01_immutability;

import lombok.Value;
import lombok.experimental.Wither;
import lombok.val;

import static java.lang.System.out;

@Value
@Wither
class Person {
    private int age;
    private String name;
}

public class _02_ImmutableObjects {
    public static void main(String[] args) {
        val originalPerson = new Person(1, "1");
        val equalPerson = new Person(1, "1");

        Person personWithModifiedAge = originalPerson.withAge(12);

        if(!originalPerson.equals(equalPerson)) {
            throw new RuntimeException();
        } else {
            out.println("OK");
        }

        if(originalPerson.equals(personWithModifiedAge)) {
            throw new RuntimeException();
        }
    }


}

