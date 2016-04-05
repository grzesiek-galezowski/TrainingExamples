package p04_mocks;

import org.testng.annotations.Test;

import static org.mockito.Mockito.mock;

public class Ex06MockingRealClasses {
  @Test //please don't mock real classes!
  public void shouldCreateMockOfRealClasses() {
    Lolek mock1 = mock(Lolek.class);
    //Lolek mock2 = mock(Lolek.class, withSettings().useConstructor());

  }

  class Lolek {
    public Lolek() {
      throw new RuntimeException("a");
    }
  }
}
