package p02_assertions;

import org.assertj.core.api.Condition;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex01AssertThat {

  @Test
  public void shouldBeAdult1() {
    //GIVEN
    Person person = new Person();

    //WHEN
    person.setAge(18);

    //THEN
    assertThat(person.getAge()).isGreaterThanOrEqualTo(18); //show different assertions for different values
  }

  @Test
  public void shouldBeAdultWithLinkedAssertions() {
    //GIVEN
    Person person = new Person();

    //WHEN
    person.setAge(18);

    //THEN
    assertThat(person.getAge())
        .isGreaterThanOrEqualTo(18)
        .isNotEqualTo(0)
        .inHexadecimal().isGreaterThanOrEqualTo(18); //TODO change this to higher
  }


  @Test
  public void shouldBeAdultUsingExtensionPoints() {
    //GIVEN
    Person person = new Person();

    //WHEN
    person.setAge(18);

    //THEN
    assertThat(person).is(adult());
    assertThat(person).has(adultAge());
    //assertThat(person).matches(adultAgeCondition());

    //opposites - TODO, show them
    //assertThat(person).is(not(adult()));
    //assertThat(person).has(not(adultAge()));

  }

  private Condition<? super Person> adultAge() {
    return new AdultPersonCondition();
  }

  private Condition<? super Person> adult() {
    return new AdultPersonCondition();
  }


}

class Person {
  private int age;

  public int getAge() {
    return age;
  }

  public void setAge(int age) {
    this.age = age;
  }
}
