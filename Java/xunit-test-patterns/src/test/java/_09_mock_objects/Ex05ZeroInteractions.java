package _09_mock_objects;

import lombok.val;
import org.junit.jupiter.api.Test;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.verifyZeroInteractions;

public class Ex05ZeroInteractions {
  @Test
  public void shouldDoNothingWhenExecuted() {
    //GIVEN
    val command = new NullCommand();

    //WHEN
    val core = mock(SharedCore.class);
    command.ExecuteOn(core);

    //THEN
    verifyZeroInteractions(core /* core, core */);
  }


}
