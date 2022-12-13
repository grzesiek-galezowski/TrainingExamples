package _09_mock_objects;

import lombok.val;
import org.junit.jupiter.api.Test;

import static org.mockito.Mockito.mock;

public class Ex06MockingRealClasses {
  @Test //please don't mock real classes!
  public void shouldCreateMockOfRealClasses() {
    val mock1 = mock(CanICreateThis.class);
    //CanICreateThis mock2 = mock(CanICreateThis.class, withSettings().useConstructor());
  }

  class CanICreateThis {
    public CanICreateThis() {
      throw new RuntimeException("a");
    }
  }
}
