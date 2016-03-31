package p03_any;

import autofixture.publicinterface.Any;
import autofixture.publicinterface.InstanceOf;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class Ex02GenericAny {
  @Test
  public void shouldCreateGenericInstances() {
    //WHEN
    MyClass<Integer> anonymous = Any.anonymous(new InstanceOf<MyClass<Integer>>() {});

    //THEN
    assertThat(anonymous.instance).isNotEqualTo(2); //TODO change to isEqualTo()
  }
}

