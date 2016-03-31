package p03_any;

import autofixture.publicinterface.Any;
import org.testng.annotations.Test;

import java.time.LocalDateTime;
import java.util.List;

import static autofixture.publicinterface.InlineGenerators.otherThan;
import static autofixture.publicinterface.InlineGenerators.without;
import static org.assertj.core.api.Assertions.assertThat;

public class Ex01PrimitiveTypesAndCollections {

  @Test
  public void shouldReturnItsInputWhenItIsNotNull() {
    //GIVEN
    String nonNullValue = Any.string();

    //WHEN
    String result = transform(nonNullValue);

    // THEN
    assertThat(result).isEqualTo(nonNullValue);
  }

  private String transform(String value) {
    if(value == null) {
      return "FAIL";
    } else {
      return value; //TODO make it fail
    }
  }

  @Test
  public void plainGenerators() {
    Any.intValue();
    Any.anonymous(Integer.class);

    Any.localDateTime();
    Any.anonymous(LocalDateTime.class);

    Any.booleanValue();
    Any.anonymous(Boolean.class);

    Any.charValue();
    Any.anonymous(Character.class);

    //...etc

    Any.uri();
    Any.url();
    Any.zonedDateTime();
    //...
  }

  @Test
  public void collections() {
    Any.arrayOf(Integer.class);
    Any.collectionOf(Integer.class);
    Any.sortedMapBetween(String.class, Integer.class);
    //...
  }

  @Test
  public void plainConstrainedGenerators() {
    Any.intOtherThan(1,2,4,5);
    Any.stringContaining("abc");
    Any.stringOfLength(123);
    //...
  }

  @Test //TODO pull newer version of AutoFixtureGenerator and tdd-toolkit
  public void constrainedGenerators() {
    Integer anInt = Any.anonymous(Integer.class, otherThan(123));
    List<String> stringsWithoutAbc = Any.listOf(String.class, without("abc"));
    Integer[] integers2 = Any.arrayOf(Integer.class, without(123));
  }


}
