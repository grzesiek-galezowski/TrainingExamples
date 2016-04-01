package p04_mocks;

import org.testng.annotations.Test;
import p04_mockito.NullCommand;
import p04_mockito.SharedCore;

import static org.mockito.Mockito.mock;
import static org.mockito.Mockito.verifyZeroInteractions;

public class Ex05ZeroInteractions {
  @Test
  public void shouldDoNothingWhenExecuted() {
    //GIVEN
    NullCommand command = new NullCommand();

    //WHEN
    SharedCore core = mock(SharedCore.class);
    command.ExecuteOn(core);

    //THEN
    verifyZeroInteractions(core /* core, core */);
  }


}
