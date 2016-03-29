package p01_core_runner;

import org.testng.annotations.Test;

import static org.testng.Assert.assertEquals;

public class Ex02CategoriesGroups {

  @Test(groups = {
      "SlowTest",
      "NotReallyAUnitTests",
      "JustFoolingAround"})
  public void shouldDoWhatever() {
    //GIVEN

    //WHEN

    //THEN
    assertEquals(2,2);
  }

  @Test(groups = {""}) //TODO show how known groups are suggested
  public void shouldDoWhatever2() {
    //GIVEN

    //WHEN

    //THEN
    assertEquals(2,2);
  }

}
