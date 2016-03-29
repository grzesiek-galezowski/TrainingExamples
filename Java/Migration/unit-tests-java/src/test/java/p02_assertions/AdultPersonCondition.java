package p02_assertions;

import org.assertj.core.api.Condition;

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
