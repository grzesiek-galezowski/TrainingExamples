package _07_picking_test_values;

import autofixture.publicinterface.Any;
import autofixture.publicinterface.InstanceOf;
import org.testng.annotations.Test;

import static org.assertj.core.api.Assertions.assertThat;

public class _05_DerivedValues {

    @Test
    public void shouldCreateGenericInstances() {
        //WHEN
        MyClass<Integer> anonymous = Any.anonymous(new InstanceOf<MyClass<Integer>>() {});

        //THEN
        assertThat(anonymous.instance).isNotEqualTo(2); //TODO change to isEqualTo()
    }

    class MyClass<T> {
        public T instance;

        public MyClass(T instance) {
            this.instance = instance;
        }
    }
}
