package p04_mocks;

import org.testng.annotations.Test;

import static org.mockito.Mockito.mock;

public class Ex06MockingRealClasses {
  @Test //please don't mock real classes!
  public void shouldCreateMockOfRealClasses() {
    CannotCreateThis mock1 = mock(CannotCreateThis.class);
    //CannotCreateThis mock2 = mock(CannotCreateThis.class, withSettings().useConstructor());

  }

  class CannotCreateThis {
    public CannotCreateThis() {
      throw new RuntimeException("a");
    }
  }
}
