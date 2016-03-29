package p01_core_runner;

import org.testng.annotations.DataProvider;
import org.testng.annotations.Test;

import static org.testng.Assert.assertEquals;

public class Ex03ParameterizedTests {

  @DataProvider
  public static Object[][] shouldUseExternalValuesData() {
    return new Object[][]{
        {3, 3, "What did you expect?"}, //change the values
        {3, 3, "Surprised?"}
    };
  }

  @Test(dataProvider = "shouldUseExternalValuesData")
  public void testPrimeNumberChecker(int a, int b, String message) {
    assertEquals(a,b,message);
  }

  //TODO show how tests can be generated using live templates
}
