package p02_assertions.custom_conditions;

import org.assertj.core.api.Condition;
import p02_assertions.Person;

public class AdultPersonCondition extends Condition<Person> {

  @Override
  public boolean matches(Person p) {
    int age = p.getAge();
    if(age < 18) {
      describedAs("adult person of age at least 18, but was " + age);
      return false;
    } else {
      return true;
    }
  }
}
