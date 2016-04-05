package p01_core_runner;

import org.testng.annotations.Test;

//Questions:
//1. Which one runs first?
//2. Do both pass?
public class Ex01RememberingDataBetweenTests {
  private int _i = 0;

  @Test
  public void shouldIncrementANumberOneTime() {
    _i++;
    //assertEquals(_i, 1); //note the different order in the assertion!
  }

  @Test
  public void shouldAlsoIncrementANumberOneTime() {
    _i++;
    //assertEquals(_i, 1); //note the different order in the assertion!
  }
}

